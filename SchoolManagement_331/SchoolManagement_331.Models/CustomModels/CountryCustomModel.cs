﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Models.CustomModels
{
   public  class CountryCustomModel
    {
        public int CountryID { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
