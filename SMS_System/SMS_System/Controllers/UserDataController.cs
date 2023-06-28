using Microsoft.AspNetCore.Mvc;
using SMS_System.Filters;

namespace SMS_System.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = true)]
    public class UserDataController : Controller
    {
        public IActionResult UserDetails()
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
    }
}
