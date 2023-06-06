using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models
{
    public class StateModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string StateName { get; set; }
        
        public string CountryName { get; set; }
        [Required]
        public int CountryID { get; set; }
    }
}