using Newtonsoft.Json;
using SchoolManagement_331.Helper.CityHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.Repository.Services;
using SchoolManagement_331.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
   // [Authorize]
    [Validate]
    public class CityController : Controller
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        CityHelper Helper = new CityHelper();
        CityServices services = new CityServices();
      
        public ICityInterface CityServices;
        public ICountryInterface CountryServices;
        public IStateInterface stateServices;
        public CityController(ICityInterface CityInterfacecs,ICountryInterface countryInterfacecs, IStateInterface stateInterfacecs)
        {
            stateServices = stateInterfacecs;
            CountryServices = countryInterfacecs;
            CityServices = CityInterfacecs;
        }
        // GET: City
        public ActionResult AddCity(int? Id)
        {
            try
            {

                if (Id == null)
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");                   
                    ViewBag.StateList = new SelectList(stateServices.GetStates(), "StateID", "StateName");
                    return View();

                }
                else
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    ViewBag.StateList = new SelectList(stateServices.GetStates(), "StateID", "StateName");
                    City city = services.GetCityById(Id);
                    CityCustomeModel cityCustomeModel = Helper.BindCityToCustomeCity(city);
                    return View(cityCustomeModel);
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Error","Home");
            }

        }
        [HttpPost]
        public ActionResult AddCity(int? Id, CityCustomeModel City)
        {
            try
            {
                if (Id == null)
                {

                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    ViewBag.StateList = new SelectList(stateServices.GetStates(), "StateID", "StateName");
                    services.AddCity(City, 0);
                    return RedirectToAction("ShowCity","City");

                }
                else
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    ViewBag.StateList = new SelectList(stateServices.GetStates(), "StateID", "StateName");
                    services.AddCity(City,Id);
                    return RedirectToAction("ShowCity", "City");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }

        }
        public ActionResult ShowCity()
        {

            try
            {
               var City = services.GetCities();
                return View(City);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult DeleteCity(int? id)
        {

            try
            {
               var cities = services.DeleteCity(id);
                if(cities == 0)
                {
                    TempData["Error"] = "City is In Use, You Can't Delete It!.......";
                }
                return RedirectToAction("ShowCity");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }



        }
        public JsonResult GetStates(int id)
        {
            try
            {
          
                db.Configuration.ProxyCreationEnabled = false;
                List<State> data = db.State.Where(x => x.CountryID == id).ToList();
                var jsonString = JsonConvert.SerializeObject(data);
                return Json(jsonString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}