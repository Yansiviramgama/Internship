using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.FormDetailsHelper
{
    public class FormDetailsHelper
    {
        public Form_Data BindCustomeFormDataToFormData(FormDetailsCustomeModel customeModel)
        {
            Form_Data form = new Form_Data()
            {
                UserID = customeModel.UserID,
                FirstName = customeModel.FirstName,
                LastName = customeModel.LastName,
                Email = customeModel.Email,
                ContactNumber = customeModel.ContactNumber,
                Gender = customeModel.Gender,
                BirthDate = customeModel.BirthDate,
                ADDRESS = customeModel.Address,
                UserCountry = Convert.ToInt32(customeModel.Country),
                UserState = Convert.ToInt32(customeModel.State),
                UserCity = Convert.ToInt32(customeModel.City),
                PostalCode = customeModel.PostalCode

            };
            return form;
        }

        public FormDetailsCustomeModel BindFormDataToCustomeFormData(Form_Data Model)
        {
            FormDetailsCustomeModel form = new FormDetailsCustomeModel()
            {
                UserID = Model.UserID,
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Email = Model.Email,
                ContactNumber = Model.ContactNumber,
                Gender = Model.Gender,
                BirthDate = (DateTime)Model.BirthDate,
                Address = Model.ADDRESS,
                CountryId= Model.UserCountry,
                StateId = Model.UserState,
                CityId = Model.UserCity,
                PostalCode = (int)Model.PostalCode

            };
            return form;
        }
    }
}
