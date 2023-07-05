using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Encodings;
using Org.BouncyCastle.Pqc.Crypto.Bike;
using System.Net.Mail;
using Yansi_KalmTraining.Model.Main;
using Yansi_KalmTraining.Model.SMTPSettings;
using Yansi_KalmTraining.Model.Viewmodel;
using Yansi_KalmTraining.Services.JWTAuthentication;
using Yansi_KalmTraining.Services.UserLogin;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Yansi_KalmTraining.Controllers
{
    public class LoginController : Controller
    {
        #region Field  
        private IUserPanelServices _userLoginServices;
        private IJWTAuthenticationServices _jwtAuthenticationServices;
        private readonly AppSettings _appSettings;
        private readonly SMTPSettings _smtpSettings;
        #endregion

        #region constructor
        public LoginController(IUserPanelServices userLoginServices, IJWTAuthenticationServices authenticationServices, IOptions<AppSettings> appSettings, IOptions<SMTPSettings> smtpSettings)
        {
            _userLoginServices = userLoginServices;
            _jwtAuthenticationServices = authenticationServices;
            _appSettings = appSettings.Value;
            _smtpSettings = smtpSettings.Value;
        }
        #endregion

        #region Login Method
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userLoginServices.UserLogin(model);
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
                        TempData["Msg"] = "Login Successfully !";
                        return RedirectToAction("CompanyDetails", "Company");
                    }
                    else
                    {
                        TempData["Login"] = "Invalid UserName/Password";
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
        #endregion

        #region LogOut Method
        public IActionResult LogOut()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region ForgotPassword Method
        public IActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(UserResponseModel model)
        {
            try
            {
                var data = await _userLoginServices.GetUserDatabyEmail(model.User_Email);
                if (data != null)
                {
                    var email = new MimeMessage();
                    email.Sender = MailboxAddress.Parse(_smtpSettings.FromEmail);
                    email.To.Add(MailboxAddress.Parse(data.User_Email));
                    email.Subject = "Forgot Id/Password OTP Email";
                    var builder = new BodyBuilder();

                    Random rand = new Random();
                    int Code = rand.Next(999999);

                    await _userLoginServices.UpdateOTP(Code, data.UserID);

                    builder.HtmlBody = $"<p>TOM-Kalm-Company-Portal</p><br><p>Requested OTP for forgot password</p><br><h4>Email : {data.User_Email}</h4><br><h5>OTP : <b>{Code}<b></h5><br> ";
                    email.Body = builder.ToMessageBody();

                    using var smtp = new SmtpClient();
                    smtp.Connect(_smtpSettings.EmailHostName, Convert.ToInt32(_smtpSettings.EmailPort), SecureSocketOptions.StartTls);
                    smtp.Authenticate(_smtpSettings.FromEmail, _smtpSettings.EmailAppPassword);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);

                    TempData["Email"] = data.User_Email;
                    return RedirectToAction("AddOTP");
                }
                else
                {
                    ViewBag.Error = "Email not register";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region OTP Method
        public IActionResult AddOTP()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOTP(OTPModel OTPData)
        {
            try
            {
                var data = await _userLoginServices.VerifyOTP(OTPData.OTP, TempData["Email"].ToString());
                TempData.Keep("Email");
                if (data == 1)
                {
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    TempData["Message"] = "Invalid OTP";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region ChangePassword Method
        public IActionResult ChangePassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var data = await _userLoginServices.UpdatePassword(model.NewPassword, TempData["Email"].ToString());
                TempData.Keep("Email");
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        #endregion
    }
}
