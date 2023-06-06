using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.Models
{
    public class ForgotPassword
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter Valid Email")]
        public string Email { get; set; }
    }
}