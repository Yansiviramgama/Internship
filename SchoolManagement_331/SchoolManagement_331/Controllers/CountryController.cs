using SchoolManagement_331.Helper.CountryHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
    public class CountryController : Controller
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        CountryHelper Hepler = new CountryHelper();
        public ICountryInterface CountryServices;
        public CountryController(ICountryInterface interfacecs)
        {
            CountryServices = interfacecs;

        }
        // GET: Country
        public ActionResult AddCountry(int ? id)
        {
            if(id == 0){
                return View();
            }
            else
            {
                Country country = CountryServices.GetCountryById(id);
                CountryCustomModel countryCustom = Hepler.BindCountryToCustomeCountry(country);
                return View(countryCustom);
            }
           

        }
        [HttpPost]
        public ActionResult AddCountry(int ? id, CountryCustomModel country)
        {
            if (id == null)
            {
                CountryServices.AddCountry(country, 0);
                
                return RedirectToAction("ShowCountry", "Country");
            }
            else
            {
                CountryServices.AddCountry(country, id);
                return RedirectToAction("ShowCountry", "Country");
            }

        }
        public ActionResult ShowCountry()
        {
            try
            {
                return View(CountryServices.GetCountries());
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public ActionResult DeleteCountry(int? id)
        {

            try
            {
                var countryid = CountryServices.DeleteCountry(id);
                if (countryid == 0)
                {
                    TempData["Error"] = "Country is In Use, You Can't Delete It!.......";
                }

               

                return RedirectToAction("ShowCountry");
            }
            catch (Exception)
            {

                return View("Error");
            }



        }

    }
}
