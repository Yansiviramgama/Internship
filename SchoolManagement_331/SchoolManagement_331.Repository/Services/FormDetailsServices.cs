using SchoolManagement_331.Helper.FormDetailsHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Services
{
    public class FormDetailsServices : IFormDetailsInterface
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        FormDetailsHelper Helper = new FormDetailsHelper();
        public bool AddFormDetails(FormDetailsCustomeModel formDetailsCustome, int? id)
        {
            Form_Data main = Helper.BindCustomeFormDataToFormData(formDetailsCustome);
            if(main != null)
            {
                if(id == null)
                {
                    db.Sp_AddEdit_Data(null,formDetailsCustome.FirstName,
                        formDetailsCustome.LastName,
                        formDetailsCustome.Email,
                        formDetailsCustome.ContactNumber,
                        formDetailsCustome.BirthDate,
                        formDetailsCustome.Gender,
                        formDetailsCustome.Address,
                        Convert.ToInt32(formDetailsCustome.Country),
                       Convert.ToInt32(formDetailsCustome.State),
                        Convert.ToInt32(formDetailsCustome.City) ,
                        formDetailsCustome.PostalCode);
                    return true;
                }
                else
                {
                    db.Sp_AddEdit_Data(formDetailsCustome.UserID, 
                        formDetailsCustome.FirstName,
                        formDetailsCustome.LastName,
                        formDetailsCustome.Email,
                        formDetailsCustome.ContactNumber,
                        formDetailsCustome.BirthDate,
                        formDetailsCustome.Gender,
                        formDetailsCustome.Address,
                         Convert.ToInt32(formDetailsCustome.Country),
                       Convert.ToInt32(formDetailsCustome.State),
                        Convert.ToInt32(formDetailsCustome.City),
                        formDetailsCustome.PostalCode);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public int DeleteUser(int? id)
        {
           var deletedata =  db.Sp_Delete_Data(id);
            return deletedata;
        }

        public Form_Data GetUserById(int? id)
        {
            var userid = db.Form_Data.Where(x => x.UserID == id).FirstOrDefault();
            return userid;
        }

        //public List<FormDetailsCustomeModel> GetUsers()
        //{

        //    //var listofuser = db.Form_Data.ToList();
        //    var query = (from s in db.Form_Data
        //                join cn in db.Country on s.UserCountry equals cn.CountryID
        //                join st in db.State on s.UserState equals st.StateID
        //                join ct in db.City on s.UserCity equals ct.CityID
        //                select new FormDetailsCustomeModel
        //                {
        //                    UserID = s.UserID,
        //                    FirstName = s.FirstName,
        //                    LastName = s.LastName,
        //                    Email = s.Email,
        //                    ContactNumber = s.ContactNumber,
        //                    BirthDate = s.BirthDate,
        //                    Gender = s.Gender,
        //                    Address = s.ADDRESS,
        //                    Country = cn.CountryName,
        //                    State = st.StateName,
        //                    City = ct.CityName,
        //                    PostalCode = (int)s.PostalCode
        //                }).ToList();
        //    return query;
        //}
        public List<Form_Data> GetUsers()
        {
            var listofuser = db.Form_Data.ToList();
            return listofuser;
        }
    }
}
