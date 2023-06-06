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
    public class StateController : Controller
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        // GET: State
        public ActionResult AddState(int ? id)
        {
            try
            {
                if (id == null)
                {

                    List<Stu_Country> country;
                    country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    return View();

                }
                else
                {

                    List<Stu_Country> country = db.Stu_Country.ToList();
                    var State = db.Stu_State.Where(x => x.ID == id).FirstOrDefault();
                    StateModel Statemodel = new StateModel()
                    {
                        ID = State.ID,
                        CountryID = (int)State.CountryID,
                        StateName = State.StateName
                    };
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    return View(Statemodel);
                }

            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AddState(int? id, Stu_State State)
        {

            try
            {
                if (id == null)
                {
                    List<Stu_Country> country = db.Stu_Country.ToList();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    db.Sp_AddEdit_State(null, State.StateName, State.CountryID);
                    return RedirectToAction("ShowState");

                }
                else
                {
                    List<Stu_Country> country;
                    country = db.Stu_Country.ToList();
                    var state = db.Stu_State.Where(x => x.ID == id).FirstOrDefault();
                    ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                    db.Sp_AddEdit_State(State.ID, State.StateName, State.CountryID);
                    return RedirectToAction("ShowState");
                }
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        public ActionResult ShowState()
        {

            try
            {
                return View(db.Stu_State.ToList());
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        public ActionResult DeleteState(int ? Id)
        {
            try
            {
                db.Sp_Delete_State(Id);
                return RedirectToAction("ShowState");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
       
    }
}