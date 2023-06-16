using Newtonsoft.Json;
using SchoolManagement_331.Helper.StateHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.Repository.Services;
using SchoolManagement_331.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
   // [Authorize]
    [Validate]
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
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }
        }
        public  ActionResult ShowState()
            {
            try
            {
                var States = services.GetStates();
                return View(States);
                //IEnumerable<State> cn = null;
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("http://localhost:51118/Api/State");



                //    var responseTask = client.GetAsync("State");
                //    responseTask.Wait();



                //    var result = responseTask.Result;


                //    if (result.IsSuccessStatusCode)
                //    {
                //        var readTask = result.Content.ReadAsAsync<List<State>>();
                //        readTask.Wait();
                //        cn = readTask.Result;
                //    }
                //    else
                //    {
                //        cn = Enumerable.Empty<State>();
                //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //    }
                //}
                //return View(cn);



                //return View();
            }
            catch 
            {
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }
        }

    }
}