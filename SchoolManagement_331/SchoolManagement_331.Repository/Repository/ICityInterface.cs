using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
   public interface ICityInterface
    {
        bool AddCity(CityCustomeModel city, int? id);
        City GetCityById(int? id);
        List<City> GetCities();
        int DeleteCity(int? id);
    }
}
