using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolManagement_331.Controllers
{
   
    public class LoginController : Controller
    {
        public IUserPanelInterfacecs userPanel;
        public LoginController(IUserPanelInterfacecs interfacecs)
        {
            userPanel = interfacecs;
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginCustomModel user)
        {
            try
            {
                if (userPanel.Login(user) == true)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Dashboard","Home");
                }
                else
                {
                    TempData["Error"] = "Invalid User Or Password";
                    return View();
                }
               
            }

            catch
            {
                return View();
            }
        }


        public ActionResult LogOut()
        {

            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        
        public ActionResult SignUp()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpCustomModel user)
        {
            try
            {              
               
                if (userPanel.SignUp(user)==true)
                {
                    return RedirectToAction("Login", "Login");
                }
                return View();
            }
            catch
            {
                return RedirectToAction("SignUp", "Login");
            }

        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(SignUpCustomModel customModel)
        {

            var forgot = userPanel.ForgotPassword(customModel);
            if(forgot != null)
            {
                WebMail.Send(forgot.User_Email, "Forgot Id Password", "<h3>Id Password For Login in School Management System<h3><br><br><h4>Email Address : " + forgot.User_Email + "</h4><br><h4>Password : " + forgot.User_Password + "</h4>", null, null, null, true, null, null, null, null, null, null);
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["Error"] = "Email Does Not Register...";
                return View();
            }
        }
    }
}