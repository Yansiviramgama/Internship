using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yansi_KalmTraining.Filters;
using Yansi_KalmTraining.Models;

namespace Yansi_KalmTraining.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}