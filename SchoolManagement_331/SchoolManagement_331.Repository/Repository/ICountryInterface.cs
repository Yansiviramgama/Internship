using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
   public  interface ICountryInterface
    {
       bool AddCountry(CountryCustomModel Country,int ? id);
        Country GetCountryById(int ? id);
        List<Country> GetCountries();
        int DeleteCountry(int? id);
    }
}
