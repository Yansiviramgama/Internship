using SchoolManagement_331.Helper.GlobalEnum;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.SessionHelper;
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

            try
            {
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginCustomModel user)
        {
            try
            {
                int userlogin = userPanel.Login(user);
                if (userlogin  == 0)
                {
                    User user1 = userPanel.getUserbyEmail(user.Email);
                    //FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["UserEmail"] = user.Email;
                    SessionData.UserEmail = user.Email;
                    Session["UserName"] = user1.User_Name;
                    SessionData.UserName = user1.User_Name;
                    Session["UserID"] = user1.UserID;
                    SessionData.UserID = user1.UserID;
                    Session["UserPassword"] = user1.User_Password;
                    SessionData.UserPassword = user1.User_Password;
                    Session["UserRole"] = user1.User_Role;
                    SessionData.UserRole = Enum.Parse(typeof(RoleType), user1.User_Role.ToString()).ToString();
                    return RedirectToAction("Dashboard","Home");
                }
                else if(userlogin == 2)
                {
                    TempData["Error"] = "Invalid User";
                    return View();
                }
                else if(userlogin == 1)
                {
                    TempData["Error"] = "Invalid Password";
                    return View();
                }
                else if(userlogin == 3)
                {

                    TempData["Error"] = "Invalid User Or Password";
                    return View();
                }
                else
                {                   
                    return View();
                }
               
            }

            catch(Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult LogOut()
        {

            try
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        
        public ActionResult SignUp()
        {

            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
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
                else
                {
                    TempData["Error"] = "Email Is Already Exists";
                    return View();
                }
                
            }
            catch
            {
                return RedirectToAction("SignUp", "Login");
            }

        }
        public ActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult ForgotPassword(SignUpCustomModel customModel)
        {
            try
            {

                var forgot = userPanel.ForgotPassword(customModel);
                if (forgot != null)
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
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}