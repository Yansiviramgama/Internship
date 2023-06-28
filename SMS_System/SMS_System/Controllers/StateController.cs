using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMS_System.Model.ViewModel;
using SMS_System.Services.Country;
using SMS_System.Services.State;
using SMS_System.Filters;

namespace SMS_System.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = true)]
    public class StateController : Controller
    {
        public IStateServices _stateServices;
        public ICountryServices _CountryServices;
        public StateController(IStateServices stateServices,ICountryServices country)
        {
            _stateServices = stateServices;
            _CountryServices = country;
        }
        public async Task<IActionResult> AddEditState(int id)
        {
            try
            {
                if (id == 0)
                {
                    ViewBag.CountryList = new SelectList(await _CountryServices.GetAllCountry(), "CountryId", "CountryName");
                    return View();
                }
                else
                {
                    ViewBag.CountryList = new SelectList(await _CountryServices.GetAllCountry(), "CountryId", "CountryName");
                    var data = await _stateServices.GetStateById(id);
                    return View(data);
                }
            }
            catch (Exception)
            {
                throw;
            }          
        }
        [HttpPost]
        public async Task<IActionResult> AddEditState(StateModel state)
        {
            try
            {
                if (state != null)
                {
                    await _stateServices.AddEditState(state);
                    return RedirectToAction("StateList");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> StateList()
        {
            try
            {
                var StateList = await _stateServices.GetAllState();
                return View(StateList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> DeleteState(int id) 
        {
            try
            {
               var index = await _stateServices.DeleteState(id);
                if(index != 0)
                {
                    TempData["Error"] = "State In Use You Can't Delete It !";
                    return RedirectToAction("StateList");
                }
                return RedirectToAction("StateList");
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
