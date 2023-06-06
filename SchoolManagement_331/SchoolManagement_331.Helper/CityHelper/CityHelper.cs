﻿using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.CityHelper
{
   public class CityHelper
    {
        public City BindCustomeCityToCity(CityCustomeModel city)
        {


            City Citymodel = new City()
            {
                CityID = city.CityID,
                CityName = city.CityName,
                StateID = city.StateID,
                CountryID = city.CountryID


            };
            return Citymodel;

        }
        public CityCustomeModel BindCityToCustomeCity(City city)
        {

            CityCustomeModel model = new CityCustomeModel()
            {
                CityID = city.CityID,
                CityName = city.CityName,
                StateID = city.StateID,
                CountryID = city.CountryID
            };


            return model;

        }
    }
}
