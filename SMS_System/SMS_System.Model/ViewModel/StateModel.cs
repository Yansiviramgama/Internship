using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Model.ViewModel
{
    public class StateModel
    {
        
        public int StateId { get; set; }       
        public int CountryId { get; set; }
        [Required]
        public string? StateName { get; set; }
        [Required]
        public string? CountryName { get; set; }

    }
}
