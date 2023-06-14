using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model.CustomModel
{
   public class StateCustomModel
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
