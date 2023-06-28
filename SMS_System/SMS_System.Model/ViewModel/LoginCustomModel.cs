using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Model.ViewModel
{
    public class LoginCustomModel
    {
       
        [Required]
        public string? UserEmail { get; set;}
        [Required]
        public string? Password { get; set;} 
    }
    public class UserDataModel
    {
        public int UserID { get; set; }
     
        public required string User_Name { get; set;}
        [Required]
        public required string User_Email { get; set;}

        [Required]
        public string? User_Password { get; set;}
		
		
		public string? CONFIRMPASSWORD { get; set; }
	}
    public class ForgotDetails
    {
        public int UserID { get; set; }     
        [Required]
        public string? USERNAME { get; set; }
        [Required]
        public string? EMAILID { get; set; }
        [Required]
        public string? PASSWORD { get; set; }
    }
}
