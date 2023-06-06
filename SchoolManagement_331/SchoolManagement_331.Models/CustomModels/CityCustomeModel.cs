using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Models.CustomModels
{
   public class CityCustomeModel
    {
        [Required]
        public int CityID { get; set; }
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
