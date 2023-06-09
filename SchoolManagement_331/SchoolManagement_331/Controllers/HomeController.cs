using SchoolManagement_331.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
    //[Authorize]
    [Validate]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
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
        public ActionResult Error()
        {
           
                return View();
          
        }
    }
}