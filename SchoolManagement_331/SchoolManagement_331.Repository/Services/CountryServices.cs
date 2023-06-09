using SchoolManagement_331.Helper.CountryHelper;
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
    public class CountryServices : ICountryInterface
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        CountryHelper Helper = new CountryHelper();
      

        public bool AddCountry(CountryCustomModel Country, int ? id)
        {
            try
            {
                Country main = Helper.BindCustomeCountryToCountry(Country);
                if (main != null)
                {
                    if (id == 0)
                    {
                        var countryData = db.Country.Any(x => x.CountryName.ToLower() == Country.CountryName.ToLower());
                        if (countryData == false)
                        {
                            db.Sp_AddEdit_Country(0, Country.CountryName);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        db.Sp_AddEdit_Country(Country.CountryID, Country.CountryName);
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        public int DeleteCountry(int? id)
        {
            try
            {
                return db.Sp_Delete_Country(id);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Country> GetCountries()
        {
            try
            {
                return db.Country.ToList();
            }
            catch 
            {
                return null;
            }
        }

        public Country GetCountryById(int ? id)
        {
            try
            {
                return db.Country.Where(x => x.CountryID == id).FirstOrDefault();
            }
            catch 
            {
                return null;
            }
        }
    }
}
