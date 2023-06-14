using Login.Model.DbContext;
using Login.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
   
    public class HomeController : Controller
    {
        public ILoginInterface loginInterface;
        public HomeController(ILoginInterface login)
        {
            loginInterface = login;
        }
            public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Student_User student)
        {
            int login = loginInterface.StudentLogin(student);
            if(login == 0)
            {
                return RedirectToAction( "About", "Home");
            }
            else if(login == 1)
            {
                TempData["Error"] = "Invalid Password";
                return View();
            }
            else if (login == 2)
            {
                TempData["Error"] = "Invalid Email";
                return View();
            }
            else
            {
                TempData["Error"] = "Invalid Email Or Password";
                return View();
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}