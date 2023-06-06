﻿using SchoolManagement_331.Helper.FormDetailsHelper;
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
                        formDetailsCustome.CountryId,
                        formDetailsCustome.StateId,
                        formDetailsCustome.CityId,
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

        public List<Form_Data> GetUsers()
        {
            var listofuser = db.Form_Data.ToList();
            return listofuser;
        }
    }
}
