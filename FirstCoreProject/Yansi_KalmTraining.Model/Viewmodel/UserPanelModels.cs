using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yansi_KalmTraining.Model.Viewmodel
{
    public class UserPanelModels
    {
        

    }
    public class UserLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string User_Email { get; set; }
        [Required]
        public string User_Password { get; set; }
    }
    public class UserResponseModel
    {
        public int UserID { get; set; }
        public required string User_Name { get; set; }
        [Required]
        public required string User_Email { get; set; }
        [Required]
        public string? User_Password { get; set; }
       // public string? CONFIRMPASSWORD { get; set; }
    }
    public class OTPModel
    {
        [Required]
        public int OTP { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$",ErrorMessage = "Password must meet requirements")]
        public string? NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password Doesn't match!")]
        public string? ConfirmPassword { get; set; }
    }
}
