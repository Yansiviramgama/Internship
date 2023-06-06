using SchoolSystem.Database;
using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSystem.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        // GET: Country
        public ActionResult AddCountry(int ? id)
        {
            try
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    var Country = db.Stu_Country.Where(x => x.ID == id).FirstOrDefault();
                    CountryModel Countrymodel = new CountryModel()
                    {
                        ID = Country.ID,
                        CountryName = Country.CountryName


                    };

                    return View(Countrymodel);
                }
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult AddCountry(int? id, Stu_Country country)
        {
            try
            {
                if (id == null)
                {
                    db.Sp_AddEdit_Country(null, country.CountryName);

                    return View();
                }
                else
                {
                    db.Sp_AddEdit_Country(country.ID, country.CountryName);

                    return View();
                }
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        public ActionResult ShowCountry()
        {

            try
            {
                return View(db.Stu_Country.ToList());
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        public ActionResult DeleteCountry(int? id)
        {

            try
            {
                db.Sp_Delete_Country(id);

                return RedirectToAction("ShowCountry");
            }
            catch (Exception)
            {

                return View("Error");
            }



        }

    }
}