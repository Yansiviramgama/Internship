﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model.CustomModel
{
    public class CountryCustomModel
    {
        public int CountryID { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
