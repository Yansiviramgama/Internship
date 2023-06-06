using Newtonsoft.Json;
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
    public class StudentController : Controller
    {

        YansiViramgama325Entities db = new YansiViramgama325Entities();
        // GET: Student
        public ActionResult AddStudent(int ? id)
        {
            if(id == null)
            {
                List<Stu_Country> Country;
                Country = db.Stu_Country.ToList();
                ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                List<Stu_State> State;
                State = db.Stu_State.ToList();
                ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                List<Stu_City> City;
                City = db.Stu_City.ToList();
                ViewBag.CityList = new SelectList(db.Stu_City.ToList(), "ID", "CityName");
                return View();
            }
            else
            {

                List<Stu_Country> Country;
                Country = db.Stu_Country.ToList();
                ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                List<Stu_State> State;
                State = db.Stu_State.ToList();
                ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                List<Stu_City> City;
                City = db.Stu_City.ToList();
                ViewBag.CityList = new SelectList(db.Stu_City.ToList(), "ID", "CityName");
                var Data = db.Student_Data.Where(x => x.ID == id).FirstOrDefault();
                StudentForm StudentData = new StudentForm()
                {
                    ID = Data.ID,
                    FirstName = Data.FirstName,
                    LastName = Data.LastName,
                    Email = Data.Email,
                    ContactNum = Data.ContactNumber,
                    BirthDate = (DateTime)Data.BirthDate,
                    Gender = Data.Gender,
                    Address = Data.ADDRESS,
                    CountryId = (int)Data.Country,
                    StateId = (int)Data.State,
                    CityId = (int)Data.City,
                    PostalCode = (int)Data.PostalCode
                };

                return View(StudentData);
            }
          
        }

        [HttpPost]
        public ActionResult AddStudent(int?id, StudentForm Student)
        {
            if (id == null)
            {
                List<Stu_Country> Country;
                Country = db.Stu_Country.ToList();
                ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                List<Stu_State> State;
                State = db.Stu_State.ToList();
                ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                List<Stu_City> City;
                City = db.Stu_City.ToList();
                ViewBag.CityList = new SelectList(db.Stu_City.ToList(), "ID", "CityName");
                //var Data = db.Student_Data.Where(x => x.ID == id).FirstOrDefault();
                // StudentForm StudentData = new StudentForm()
                //{
                //    ID = Data.ID,
                //    FirstName = Data.FirstName,
                //    LastName = Data.LastName,
                //    Email = Data.Email,
                //    ContactNum = Data.ContactNumber,
                //    BirthDate = (DateTime)Data.BirthDate,
                //    Gender = Data.Gender,
                //    Address = Data.ADDRESS,
                //    CountryId = (int)Data.Country,
                //    StateId = (int)Data.State,
                //    CityId = (int)Data.City,
                //    PostalCode = (int)Data.PostalCode
                //};
                db.Sp_AddEdit_StudentData(null, Student.FirstName, Student.LastName, Student.Email, Student.ContactNum, Student.BirthDate, Student.Gender, Student.Address, Convert.ToInt32(Student.Country), Convert.ToInt32(Student.State),
                    Convert.ToInt32(Student.City), Student.PostalCode);
                return RedirectToAction("ShowStudent");

            }
            else
            {
                List<Stu_Country> Country;
                Country = db.Stu_Country.ToList();
                ViewBag.CountryList = new SelectList(db.Stu_Country.ToList(), "ID", "CountryName");
                List<Stu_State> State;
                State = db.Stu_State.ToList();
                ViewBag.StateList = new SelectList(db.Stu_State.ToList(), "ID", "StateName");
                List<Stu_City> City;
                City = db.Stu_City.ToList();
                ViewBag.CityList = new SelectList(db.Stu_City.ToList(), "ID", "CityName");
                var Data = db.Student_Data.Where(x => x.ID == id).FirstOrDefault();
               
                db.Sp_AddEdit_StudentData(Student.ID, Student.FirstName, Student.LastName, Student.Email, Student.ContactNum, Student.BirthDate, Student.Gender, Student.Address, Convert.ToInt32(Student.Country), Convert.ToInt32(Student.State),
                    Convert.ToInt32(Student.City), Student.PostalCode);
                return RedirectToAction("ShowStudent");
            }

        }
        public ActionResult ShowStudent()
        {
           var Data = db.Student_Data.ToList();
            return View(Data);
        }
        public ActionResult StudentDetail(int id)
        {
            List<Student_Data> Details;
            Details = db.Student_Data.ToList();
            var Data = db.Student_Data.Where(x => x.ID == id).FirstOrDefault();
            
            return View(Data);
        }
        public ActionResult DeleteStudent(int? id)
        {

            try
            {
                db.Sp_Delete_StudentData(id);

                return RedirectToAction("ShowStudent");
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
        public JsonResult GetCity(int id)
        {
            try
            {
                YansiViramgama325Entities db = new YansiViramgama325Entities();
                db.Configuration.ProxyCreationEnabled = false;
                List<Stu_City> data = db.Stu_City.Where(x => x.StateID == id).ToList();
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