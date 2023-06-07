using SchoolManagement_331.Helper.StateHelper;
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
    public class StateController : Controller
    {

        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        StateHelper Helper = new StateHelper();
        StateServices services = new StateServices();
        public IStateInterface StateServices;
        public ICountryInterface CountryServices;
        public StateController(IStateInterface interfacecs, ICountryInterface countryInterface)
        {
            StateServices = interfacecs;
            CountryServices = countryInterface;
        }
        // GET: State
        public ActionResult AddState(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    return View();
                }
                else
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    State state = services.GetStateById(id);
                    StateCustomeModel stateCustome = Helper.BindStateToCustomeState(state);
                    return View(stateCustome);
                }

               
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AddState(int? id, StateCustomeModel State)
        {

            try
            {
                if (id == null)
                {
                    StateServices.AddState(State, 0);

                    return RedirectToAction("ShowState", "State");
                }
                else
                {
                    StateServices.AddState(State, id);
                    return RedirectToAction("ShowState", "State");
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
                var States = services.GetStates();
                return View(States);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        public ActionResult DeleteState(int? Id)
        {
            try
            {
                var state = services.DeleteState(Id);
                if(state == 0)
                {
                    TempData["Error"] = "State is In Use, You Can't Delete It!.......";
                }
                return RedirectToAction("ShowState");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}