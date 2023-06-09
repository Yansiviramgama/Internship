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
            try
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
            catch
            {
                return false;
            }
        }

        public int DeleteCity(int ? id)
        {
            try
            {
                var deletecity = db.Sp_Delete_City(id);
                return deletecity;
            }
            catch 
            {
                return -1;
            }
        }

        public List<City> GetCities()
        {
            try
            {
                var getCity = db.City.ToList();
                return getCity;
            }
            catch 
            {
                return null;
            }
        }

        public City GetCityById(int? id)
        {
            try
            {
                var citybyId = db.City.Where(x => x.CityID == id).FirstOrDefault();
                return citybyId;
            }
            catch 
            {
                return null;
            }
        }

        public List<City> GetCityByState(int? id)
        {
            try
            {
                var city = db.City.Where(x => x.StateID == id).ToList();
                return city;
            }
            catch 
            {
                return null;
            }
        }
    }
}
