using SchoolManagement_331.Helper.CityHelper;
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
    public class CityServices : ICityInterface
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        CityHelper Helper = new CityHelper();
        public bool AddCity(CityCustomeModel city, int? id)
        {
            City main = Helper.BindCustomeCityToCity(city);
            if (main != null)
            {
                if (id == 0)
                {
                    db.Sp_AddEdit_City(null, city.CityName, city.StateID, city.CountryID);
                    return true;
                }
                else
                {
                    db.Sp_AddEdit_City(city.CityID, city.CityName, city.StateID, city.CountryID);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public int DeleteCity(int ? id)
        {
            var deletecity = db.Sp_Delete_City(id);
           return deletecity;
        }

        public List<City> GetCities()
        {
            var getCity = db.City.ToList();
            return getCity;
        }

        public City GetCityById(int? id)
        {
            var citybyId = db.City.Where(x => x.CityID == id).FirstOrDefault();
            return citybyId;
        }
    }
}
