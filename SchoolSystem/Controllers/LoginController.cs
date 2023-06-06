using SchoolSystem.Database;
using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            
               
            return View();
        }
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult SignUp()
        {
            using (YansiViramgama325Entities db = new YansiViramgama325Entities())
            {
                ViewBag.RoleList = new SelectList(db.Roles.ToList(), "ID", "User_Role");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login user)
        {
            try
            {    
                using (YansiViramgama325Entities db = new YansiViramgama325Entities())
                {
                 if(db.Student_User.Any(x => x.Email == user.Email && x.Password == user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Invalid User / Password";
                        //ViewBag.errormessage = "Invalid User / Password";
                        return View();
                    }
                }

                }
                   
            catch
            {
                return View();
            }
        }
      
        [HttpPost]
        public ActionResult SignUp(SignUp user)
        {
            try
            {
                using (YansiViramgama325Entities db = new YansiViramgama325Entities())
                {
                    
                    db.Student_User.Add(new Student_User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        User_Role = user.Role
                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Login", "Login");
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