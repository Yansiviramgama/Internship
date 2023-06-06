using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class CityModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string CityName { get; set; }
        public string StateName { get; set; }
        [Required]
        public int StateID { get; set; }
        public string CountryName { get; set; }
        [Required]
        public int CountryID { get; set; }
    }
}