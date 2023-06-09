using Newtonsoft.Json;
using SchoolManagement_331.Helper.FormDetailsHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
    //[Authorize]
    [Validate]
    public class StudentController : Controller
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        FormDetailsHelper Helper = new FormDetailsHelper();
        public IFormDetailsInterface formDetails;
        public ICityInterface CityServices;
        public ICountryInterface CountryServices;
        public IStateInterface stateServices;
        public StudentController (IFormDetailsInterface detailsInterface, ICityInterface CityInterfacecs, ICountryInterface countryInterfacecs, IStateInterface stateInterfacecs)
        {
            formDetails = detailsInterface;
            stateServices = stateInterfacecs;
            CountryServices = countryInterfacecs;
            CityServices = CityInterfacecs;
        }
        // GET: Student
        public ActionResult DetailsForm(int ? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    ViewBag.StateList = new SelectList("");
                    ViewBag.CityList = new SelectList("");
                    return View();
                }
                else
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    Form_Data data = formDetails.GetUserById(id);
                    ViewBag.Date = data.BirthDate.ToString("yyyy-MM-dd");
                    ViewBag.StateList = new SelectList(stateServices.GetStatesbyCountry(data.UserCountry), "StateID", "StateName");
                    ViewBag.CityList = new SelectList(CityServices.GetCityByState(data.UserState), "CityID", "CityName"); ;

                    FormDetailsCustomeModel model = Helper.BindFormDataToCustomeFormData(data);
                    return View(model);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }
      
        [HttpPost]
        public ActionResult DetailsForm(int ? Id,FormDetailsCustomeModel customeModel)
        {
            try
            {
                if (Id == null)
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    formDetails.AddFormDetails(customeModel, null);
                    return RedirectToAction("ShowStudent", "Student");
                }
                else
                {
                    ViewBag.CountryList = new SelectList(CountryServices.GetCountries(), "CountryID", "CountryName");
                    formDetails.AddFormDetails(customeModel, Id);
                    return RedirectToAction("ShowStudent", "Student");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult ShowStudent()
        {
            try
            {
                return View(formDetails.GetUsers());
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
   
        public ActionResult DeleteStudent(int ? Id)
        {
            try
            {
                formDetails.GetUserById(Id);
                return RedirectToAction("ShowStudent", "Student");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        public ActionResult StudentDetail(int ? id)
        {
            try
            {
                formDetails.GetUsers();
                var details = formDetails.GetUserById(id);
                return View(details);
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
        public JsonResult GetCity(int id)
        {
            try
            {              
                db.Configuration.ProxyCreationEnabled = false;
                List<City> data = db.City.Where(x => x.StateID == id).ToList();
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