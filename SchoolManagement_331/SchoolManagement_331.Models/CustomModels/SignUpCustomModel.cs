
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Models.CustomModels
{
    public class SignUpCustomModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Customer Name Must Have Minimum 2 And Maximum 20 Character")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Digits are not allowed.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Enter Valid Password")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}", ErrorMessage = "Your password must be at least 5 characters long and contain at least 1 letter and 1 number")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please Enter Same Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [Required]
        public int Role { get; set; }

    }
}
