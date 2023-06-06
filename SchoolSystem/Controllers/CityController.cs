using Newtonsoft.Json;
using SchoolSystem.Database;
using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.Security;

namespace SchoolSystem.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        // GET: City
        public ActionResult AddCity(int ? Id)
        {
            try
            {

                if (Id == null)
                {
                    List<Stu_Country> Country;
                    Country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    List<Stu_State> state;
                    state = db.Stu_State.ToList();
                    ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                    return View();

                }
                else
                {
                    List<Stu_Country> Country;
                    Country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    List<Stu_State> State = db.Stu_State.ToList();
                    var city = db.Stu_City.Where(x => x.ID == Id).FirstOrDefault();
                    CityModel Citymodel = new CityModel()
                    {
                        ID = city.ID,
                        CountryID = (int)city.CountryID,
                        StateID = (int)city.StateID,
                        CountryName = city.CityName

                    };

                    ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                    return View(Citymodel);
                }
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult AddCity(int? Id,Stu_City City)
        {
            try
            {
                if (Id == null)
                {
                    List<Stu_Country> Country;
                    Country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    List<Stu_State> State = db.Stu_State.ToList();
                    ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                    db.Sp_AddEdit_City(null, City.CityName, City.StateID, City.CountryID);
                    return RedirectToAction("ShowCity");

                }
                else
                {
                    List<Stu_Country> Country;
                    Country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    List<Stu_State> State;
                    State = db.Stu_State.ToList();
                    var city = db.Stu_City.Where(x => x.ID == Id).FirstOrDefault();

                    ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                    db.Sp_AddEdit_City(City.ID, City.CityName, City.StateID, City.CountryID);
                    return RedirectToAction("ShowCity");
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
                return View(db.Stu_City.ToList());
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
                db.Sp_Delete_City(id);

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
                YansiViramgama325Entities db = new YansiViramgama325Entities();
                db.Configuration.ProxyCreationEnabled = false;
                List<Stu_State> data = db.Stu_State.Where(x => x.CountryID == id).ToList();
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