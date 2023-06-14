using Login.Model.CustomModel;
using Login.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Helper.Helpers
{
    public class CountryHelper
    {
        public static Stu_Country BindCustomeCountryToCountry(CountryCustomModel Country)
        {
            try
            {
                Stu_Country Countrymodel = new Stu_Country()
                {
                    ID = Country.CountryID,
                    CountryName = Country.CountryName
                };
                return Countrymodel;
            }
            catch
            {
                return null;
            }
        }
        public static CountryCustomModel BindCountryToCustomeCountry(Stu_Country country)
        {
            try
            {
                CountryCustomModel model = new CountryCustomModel()
                {
                    CountryID = country.ID,
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
