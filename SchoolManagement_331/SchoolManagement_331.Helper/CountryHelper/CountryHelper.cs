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

            
            Country Countrymodel = new Country()
            {
                CountryID = Country.CountryID,
                CountryName = Country.CountryName


            };
            return Countrymodel;

        }
        public CountryCustomModel BindCountryToCustomeCountry(Country country)
        {

            CountryCustomModel model = new CountryCustomModel()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
            
           
            return model;

        }
    }
}
