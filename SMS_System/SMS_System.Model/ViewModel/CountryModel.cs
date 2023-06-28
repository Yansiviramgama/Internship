using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Model.ViewModel
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        [Required]
        public string? CountryName { get; set; }
    }
}
