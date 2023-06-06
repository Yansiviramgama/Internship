using SchoolManagement_331.Helper.GlobalEnum;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.SignUpHelper
{
   public class SignUpHelper
    {
        public static User BindCustomesignUpToSignUp(SignUpCustomModel Signup)
        {
            User user = new User()
            {
                User_Name = Signup.Name,
                User_Email = Signup.Email,
                User_Password = Signup.Password,
                User_Role = Signup.Role,
            };
            return user;


        }
      
        public static string getEnumString(int Role)
        {
            return Enum.GetName(typeof(RoleType), Role);
        }
    }
}
