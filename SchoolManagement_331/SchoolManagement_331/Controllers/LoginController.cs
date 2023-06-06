using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    TempData["Error"] = "Invalid User / Password";
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
    }
}