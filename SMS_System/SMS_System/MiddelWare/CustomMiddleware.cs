using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MimeKit;
using SMS_System.Common.CommonMethod;
using SMS_System.Logger;
using SMS_System.Model.Main;
using SMS_System.Model.ViewModel;
using SMS_System.Services.JWTauthentication;
using SMS_System.Services.UserLogin;
using System.Net.Mail;
using System.Text;

namespace SMS_System.MiddelWare
{
    public class CustomMiddleware
    {
        private readonly ILoggerManager _logger;
        private readonly RequestDelegate _next;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IJWTAuthenticationServices _jwtAuthenticationService;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        public CustomMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IJWTAuthenticationServices jwtAuthenticationService,
            IConfiguration config,ILoggerManager logger)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context, IUserLoginServices _accountService)
        {
            try
            {
                var HttpContextBody = context.Request.Body;
                string requestBody = string.Empty;

                context.Request.EnableBuffering();

                using (var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: -1,
                    leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();

                    context.Request.Headers.Add("requestModel", body);
                    context.Request.Body.Position = 0;
                }
                // Delete files from folder for logs of request and response older than 7 days.
                DeleteOldReqResLogFiles();
                // Check JWT Token validity expiry
                string jwtToken = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    UserTokenModel userTokenModel = _jwtAuthenticationService.GetUserTokenData(jwtToken);
                    var result = await _accountService.ValidateUserTokenData(userTokenModel.UserId, jwtToken, userTokenModel.TokenValidTo);
                    if (result == 1)
                    {
                        if (userTokenModel != null)
                        {
                            if (userTokenModel.TokenValidTo < DateTime.UtcNow.AddMinutes(1))
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return;
                            }
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
                AddReqResLogsToLoggerFile(context);
                await _next(context);
            }
            catch (Exception ex)
            {
                bool success = await SendExceptionEmail(ex, context);
                AddExceptionLogsToFiles(ex, context, success);
            }
        }
        /// <summary>
        /// Delete files from error logs folder which is older than 7 days.
        /// </summary>
        public void DeleteOldReqResLogFiles()
        {
            var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "ReqResLogs");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                {
                    fi.Delete();
                }
            }
        }

        /// <summary>
        /// Add error logs in folder 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        public void AddExceptionLogsToFiles(Exception ex, HttpContext context, bool success)
        {
            //string DirectoryPath = "/Logs/";
            //var BasePath = Path.Combine(_hostingEnvironment.WebRootPath, DirectoryPath);

            var exfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "ExceptionLogs", "Exception_" + Path.GetFileName(DateTime.Now.ToString("dd_MM_yyyy") + ".txt"));

            string jsonLst = CommonMethods.GetKeyValues(context);
            //var FileName = Path.GetExtension(DateTime.Now.ToString("dd_MM_yyyy"));
            if (!File.Exists(exfilePath))
            {
                var myFile = File.Create(exfilePath);
                myFile.Close();
            }

            using StreamWriter sw = File.AppendText(exfilePath);
            sw.WriteLine("");
            sw.WriteLine("--------------------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ----------------------------------");
            sw.WriteLine("Requested URL: " + context.Request.Path.Value);
            sw.WriteLine("Error Message: " + ex.Message);
            sw.WriteLine("InnerException: " + ex.InnerException);
            sw.WriteLine("StackTrace: " + ex.StackTrace);
            sw.WriteLine("Request Params: " + jsonLst);
            if (ex.InnerException != null)
            {
                sw.WriteLine("Exception: " + ex.InnerException.InnerException);
            }
        }

        /// <summary>
        /// Send Exception Email
        /// </summary>
        public async Task<bool> SendExceptionEmail(Exception ex, HttpContext context)
        {

            var email = new MimeMessage();
            var builder = new BodyBuilder();
            builder.HtmlBody = $"<h2>Requested URL : {context.Request.Path.Value}</h2><br><h2>Execption : {ex.Message}</h2><br><h2>InnerException : {ex.InnerException}</h2><br> ";
            email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();
            //smtp.Connect(_smtpSettings.EmailHostName, Convert.ToInt32(_smtpSettings.EmailPort), SecureSocketOptions.StartTls);
            //smtp.Authenticate(_smtpSettings.FromEmail, _smtpSettings.EmailAppPassword);
            //var success = await smtp.SendAsync(email);
            //smtp.Disconnect(true);
            return true;

            /*ParamValue paramValues = CommonMethods.GetKeyValues(_httpContextAccessor.HttpContext);
			//string jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace(JwtBearerDefaults.AuthenticationScheme + " ", "");
			//UserTokenModel userTokenData = _jwtAuthenticationService.GetUserTokenData(jwtToken);
			//EmailNotification.EmailSetting setting = new EmailSetting
			//{
			//	EmailEnableSsl = Convert.ToBoolean(_smtpSettings.EmailEnableSsl),
			//	EmailHostName = _smtpSettings.EmailHostName,
			//	EmailPassword = _smtpSettings.EmailPassword,
			//	EmailAppPassword = _smtpSettings.EmailAppPassword,
			//	EmailPort = Convert.ToInt32(_smtpSettings.EmailPort),
			//	FromEmail = _smtpSettings.FromEmail,
			//	FromName = _smtpSettings.FromName,
			//	EmailUsername = _smtpSettings.EmailUsername,
			//};

			//string emailBody = string.Empty;
			//string BasePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.ExceptionReportPath);

			//if (!Directory.Exists(BasePath))
			//{
			//	Directory.CreateDirectory(BasePath);
			//}
			//bool isSuccess = false;

			//using (StreamReader reader = new StreamReader(Path.Combine(BasePath, Constants.ExceptionReport)))
			//{
			//	emailBody = reader.ReadToEnd();
			//}

			//string Client_URL = Convert.ToString(_config["Data:WebAppURL"]);
			//emailBody = emailBody.Replace("##LogoURL##", Constants.logoUrl);
			//emailBody = emailBody.Replace("##DateTime##", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			//emailBody = emailBody.Replace("##RequestedURL##", context.Request.GetDisplayUrl());
			//emailBody = emailBody.Replace("##ExceptionMessage##", ex.Message);
			//emailBody = emailBody.Replace("##RequestParams##", paramValues.HeaderValue);
			//emailBody = emailBody.Replace("##QueryStringParams##", paramValues.QueryStringValue);
			//emailBody = ex.InnerException != null ? emailBody.Replace("##InnerException##", ex.InnerException.ToString()) : emailBody.Replace("##InnerException##", string.Empty);
			//emailBody = userTokenData.UserId != null ? emailBody = emailBody.Replace("##UserId##", userTokenData.UserId.ToString()) : emailBody.Replace("##UserId##", string.Empty);
			//emailBody = userTokenData.UserName != null ? emailBody = emailBody.Replace("##UserName##", userTokenData.UserName.ToString()) : emailBody.Replace("##UserName##", string.Empty);
			//isSuccess = await Task.Run(() => SendMailMessage(_appSettings.ErrorSendToEmail, null, null, "Exception Log Alert !", emailBody, setting, null));
			//if (isSuccess)
			//{
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}*/
        }

        /// <summary>
        /// Handle Exception for middleware
        /// </summary>
        //private Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //	context.Response.ContentType = "application/json";
        //	context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //	return context.Response.WriteAsync(new BaseApiResponse()
        //	{
        //		Success = false,
        //		Message = exception.Message
        //	}.ToString());

        //}

        /// <summary>
        /// Store exception in logger file
        /// </summary>
        //private void AddExceptionLogsToLoggerFile(HttpContext context, Exception exception)
        //{
        //	ParamValue paramValues = CommonMethods.GetKeyValues(_httpContextAccessor.HttpContext);
        //	StringBuilder sb = new StringBuilder();
        //	sb.Append(Environment.NewLine + "Request Params: " + paramValues.HeaderValue +
        //			  Environment.NewLine + "Query String Params: " + paramValues.QueryStringValue +
        //			  Environment.NewLine + "Message: " + exception.Message);
        //	_logger.Error(sb.ToString());
        //}

        private async void AddReqResLogsToLoggerFile(HttpContext context)
        {
            try
            {
                var exfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "ReqResLogs", "ReqResLog_" + Path.GetFileName(DateTime.Now.ToString("dd_MM_yyyy") + ".txt"));
                string jsonLst = CommonMethods.GetKeyValues(context);
                //var FileName = Path.GetExtension(DateTime.Now.ToString("dd_MM_yyyy"));
                if (!File.Exists(exfilePath))
                {
                    var myFile = File.Create(exfilePath);
                    myFile.Close();
                }
                //using StreamWriter sw = File.AppendText(exfilePath);
                //sw.WriteLine("");
                //sw.WriteLine("--------------------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ----------------------------------");
                //sw.WriteLine("Requested URL: " + context.Request.Path.Value);
                //sw.WriteLine("Context request: " + context.Request.ContentType);
                using StreamWriter sw = File.AppendText(exfilePath);
                sw.WriteLine("");
                sw.WriteLine("--------------------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ----------------------------------");
                sw.WriteLine("Requested URL: " + context.Request.Path.Value);
                //sw.WriteLine("Error Message: " + ex.Message);
                //sw.WriteLine("InnerException: " + ex.InnerException);
                //sw.WriteLine("StackTrace: " + ex.StackTrace);
                sw.WriteLine("Request Params: " + jsonLst);
                //if (ex.InnerException != null)
                //{
                //	sw.WriteLine("Exception: " + ex.InnerException.InnerException);
                //}
                //using StreamReader reader = new StreamReader(context.Request.Body);
                //string requestBody = await reader.ReadToEndAsync();
                //var requestData = JsonConvert.DeserializeObject(requestBody);

                //if (requestData != null)
                //{
                //	sw.WriteLine("Context Response: " + requestData);
                //}
                //else
                //{
                //	sw.WriteLine("Context Response: UserName Not Found" );
                //}
                //if (ex.InnerException != null)
                //{
                //	sw.WriteLine("Exception: " + ex.InnerException.InnerException);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
