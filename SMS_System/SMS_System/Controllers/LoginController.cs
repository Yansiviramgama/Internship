using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using SMS_System.Model.Main;
using SMS_System.Model.ViewModel;
using SMS_System.Services.JWTauthentication;
using SMS_System.Services.UserLogin;

namespace SMS_System.Controllers
{
    public class LoginController : Controller
    {
        #region Field
        private IUserLoginServices _userLoginServices;
        private IJWTAuthenticationServices _jwtAuthenticationServices;
        private readonly AppSettings _appSettings;
        #endregion
        #region constructor
        public LoginController(IUserLoginServices userLoginServices, IJWTAuthenticationServices authenticationServices, IOptions<AppSettings> appSettings)
        {
            _userLoginServices = userLoginServices;
            _jwtAuthenticationServices = authenticationServices;
            _appSettings = appSettings.Value;
        }
        #endregion
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginCustomModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userLoginServices.LoginDetails(model);
                    if (user != null)
                    {
                        UserTokenModel objTokenData = new UserTokenModel();
                        objTokenData.EmailId = user.User_Email;
                        objTokenData.UserId = user.UserID != 0 ? user.UserID : 0;
                        objTokenData.UserName = user.User_Name;
                        AccessTokenModel objAccessTokenData = new AccessTokenModel();
                        objAccessTokenData = _jwtAuthenticationServices.GenerateTokenModel(objTokenData, _appSettings.JWTSecretKey, _appSettings.JWTValidityMinutes);
                        var data = await _userLoginServices.UpdateLoginToken(objAccessTokenData.Token, objAccessTokenData.UserId);
                        string secretkey = _appSettings.JWTSecretKey;
                        int JWTTime = _appSettings.JWTValidityMinutes;
                        //string JWTToken = _jwtAuthenticationServices.GenerateToken(user.User_Email,Convert.ToString(user.UserID), secretkey, JWTTime);
                        string JWTToken = objAccessTokenData.Token;
                        HttpContext.Session.SetString("_token", JWTToken);
                        HttpContext.Session.SetString("_Useremail", user.User_Email);
                        var checkstoredsession = HttpContext.Session.GetString("_token");
                        TempData["Login"] = "Login Successfully !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid UserName/Password";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }          
        }
        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserDataModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userinfo = await _userLoginServices.RegisterUser(user);
                    if (userinfo != -1)
                    {
                        ViewBag.Error = "User Is Already Exist !!!!!";
                        return View();
                    }
                    else if (userinfo == 1)
                    {
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ViewBag.Error = "Something Went Wrong !!!!!";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult LogOut()
        {
            try
            {           
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
               throw;
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotDetails user)
        {

            ForgotDetails success = await _userLoginServices.GetUserDetailsByEmail(user);
            if (success != null)
            {
                var email = new MimeMessage();
                //email.Sender = MailboxAddress.Parse(_smtpSettings.FromEmail);
                //email.To.Add(MailboxAddress.Parse(success.EMAILID));
                //email.Subject = "Forgot Id/Password Email";
                var builder = new BodyBuilder();
                builder.HtmlBody = $"<h2>UserName : {success.USERNAME}</h2><br><h2>Email : {success.EMAILID}</h2><br><h2>Password : {success.PASSWORD}</h2><br> ";
                email.Body = builder.ToMessageBody();
                //using var smtp = new SmtpClient();
                //smtp.Connect(_smtpSettings.EmailHostName, Convert.ToInt32(_smtpSettings.EmailPort), SecureSocketOptions.StartTls);
                //smtp.Authenticate(_smtpSettings.FromEmail, _smtpSettings.EmailAppPassword);
                //await smtp.SendAsync(email);
                //smtp.Disconnect(true);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Error = "Email not register";
                return View();
            }
        }
    }
}
