using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.CountryHelper
{
   public  class CountryHelper
    {
        public Country BindCustomeCountryToCountry(CountryCustomModel Country)
        {
            try
            {
                Country Countrymodel = new Country()
                {
                    CountryID = Country.CountryID,
                    CountryName = Country.CountryName
                };
                return Countrymodel;
            }
            catch 
            {
                return null;
            }
        }
        public CountryCustomModel BindCountryToCustomeCountry(Country country)
        {
            try
            {
                CountryCustomModel model = new CountryCustomModel()
                {
                    CountryID = country.CountryID,
                    CountryName = country.CountryName
                };
                return model;
            }
            catch
            {
                return null;
            }

        }
    }
}
