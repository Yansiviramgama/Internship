using Newtonsoft.Json;
using SchoolManagement_331.Helper.CityHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
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

                return View("Error");
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

                return View("Error");
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

                return View("Error");
            }
        }
        public ActionResult DeleteCity(int? id)
        {

            try
            {
                services.DeleteCity(id);
                return RedirectToAction("ShowCity");
            }
            catch (Exception)
            {

                return View("Error");
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