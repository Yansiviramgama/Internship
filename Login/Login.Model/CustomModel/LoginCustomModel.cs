using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Login.Model.CustomModel
{
   public class LoginCustomModel
    {
        public int ID { get; set; }
       
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
  
       
    }
}
