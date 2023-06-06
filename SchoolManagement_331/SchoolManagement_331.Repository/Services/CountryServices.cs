﻿using SchoolManagement_331.Helper.CountryHelper;
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
                        db.Sp_AddEdit_Country(0, Country.CountryName);
                        return true;
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public int DeleteCountry(int? id)
        {
           return db.Sp_Delete_Country(id);
        }

        public List<Country> GetCountries()
        {
           return db.Country.ToList();
        }

        public Country GetCountryById(int ? id)
        {

           return db.Country.Where(x => x.CountryID == id).FirstOrDefault();
        }
    }
}
