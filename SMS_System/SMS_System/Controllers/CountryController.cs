using Microsoft.AspNetCore.Mvc;
using SMS_System.Filters;
using SMS_System.Model.common;
using SMS_System.Model.ViewModel;
using SMS_System.Services.Country;
using Newtonsoft.Json;

namespace SMS_System.Controllers
{  
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    [ResponseCache(Duration = 480, Location = ResponseCacheLocation.Any, NoStore = true)]
    public class CountryController : Controller
    {
        private ICountryServices _CountryServices;
        public CountryController(ICountryServices countryServices)
        {
            _CountryServices = countryServices;
        }
        public async Task<IActionResult> AddEditCountry(int id)
        {
            try
            {
                if (id != 0)
                {
                    var country = await _CountryServices.GetCountryById(id);
                    return View(country);
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
        [HttpPost]
        public async Task<IActionResult> AddEditCountry(CountryModel country)
        {
            try
            {
                await _CountryServices.AddEditCountry(country);
                return RedirectToAction("CountryList");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CountryList()//Pagination data)
        {
            try
            {
                List<CountryModel> countrylist = await _CountryServices.GetAllCountry();

			   //var countrylist = await _CountryServices.CountryPerPage(data);
              //decimal CountryCount = await _CountryServices.CountryCount();
               //ViewBag.CountryCount = Math.Ceiling(CountryCount / 10);
                return View(countrylist);
            }
            catch (Exception ex)
            {
				throw ex;
			}
        }
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var index = await _CountryServices.DeleteCountry(id);
                if (index == -1)
                {
                    TempData["Error"] = "Country In Use You Can't Delete It !";
                    return RedirectToAction("CountryList");
                }
                else
                {
                    return RedirectToAction("CountryList");
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }
        public async Task<JsonResult> GetCoutriesPerPage(Pagination data)
        {
            try
            {
                List<CountryModel> countryModel = await _CountryServices.CountryPerPage(data);
                var Country = from c in countryModel select new { CountryName = c.CountryName, Id = c.CountryId };
                var JsonString = JsonConvert.SerializeObject(Country);
                return Json(JsonString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
