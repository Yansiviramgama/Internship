using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Models.CustomModels
{
   public class StateCustomeModel
    {
        [Required]
        public int StateID { get; set; }
        [Required]
        public string StateName { get; set; }

        public string CountryName { get; set; }
        [Required]
        public int CountryID { get; set; }
    }
}
