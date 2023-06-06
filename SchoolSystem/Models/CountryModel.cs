using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models
{
    public class CountryModel
    {
        
        public int ID { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}