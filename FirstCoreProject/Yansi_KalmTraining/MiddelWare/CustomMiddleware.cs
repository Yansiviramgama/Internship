using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using Yansi_KalmTraining.Common.CommonMethod;
using Yansi_KalmTraining.Logger;
using Yansi_KalmTraining.Model.Main;
using Yansi_KalmTraining.Model.Viewmodel;
using Yansi_KalmTraining.Services.JWTAuthentication;
using Yansi_KalmTraining.Services.UserLogin;

namespace Yansi_KalmTraining.MiddelWare
{
    public class CustomMiddleware
    {
        /// <summary>
        /// Feilds
        /// </summary>
        private readonly ILoggerManager _logger;
        private readonly RequestDelegate _next;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IJWTAuthenticationServices _jwtAuthenticationService;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        /// <summary>
        /// Counstuctor
        /// </summary>
        public CustomMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IJWTAuthenticationServices jwtAuthenticationService,
            IConfiguration config, ILoggerManager logger)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
            _jwtAuthenticationService = jwtAuthenticationService;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        /// <summary>
        /// Middeleware Invoke Method
        /// </summary>
        public async Task Invoke(HttpContext context, IUserPanelServices _accountService)
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
                string jwtToken = context.Session.GetString("_token");
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
            var exfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "ExceptionLogs", "Exception_" + Path.GetFileName(DateTime.Now.ToString("dd_MM_yyyy") + ".txt"));
            string jsonLst = CommonMethods.GetKeyValues(context);
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
        /// Send Exception
        /// </summary>
        public async Task<bool> SendExceptionEmail(Exception ex, HttpContext context)
        {
            var email = new MimeMessage();
            var builder = new BodyBuilder();
            builder.HtmlBody = $"<h2>Requested URL : {context.Request.Path.Value}</h2><br><h2>Execption : {ex.Message}</h2><br><h2>InnerException : {ex.InnerException}</h2><br> ";
            email.Body = builder.ToMessageBody();
            return true;
        }
        private async void AddReqResLogsToLoggerFile(HttpContext context)
        {
            try
            {
                var exfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "ReqResLogs", "ReqResLog_" + Path.GetFileName(DateTime.Now.ToString("dd_MM_yyyy") + ".txt"));
                string jsonLst = CommonMethods.GetKeyValues(context);
                if (!File.Exists(exfilePath))
                {
                    var myFile = File.Create(exfilePath);
                    myFile.Close();
                }
                using StreamWriter sw = File.AppendText(exfilePath);
                sw.WriteLine("");
                sw.WriteLine("--------------------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ----------------------------------");
                sw.WriteLine("Requested URL: " + context.Request.Path.Value);
                sw.WriteLine("Request Params: " + jsonLst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
