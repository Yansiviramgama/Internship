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
            try
            {
                User user = new User()
                {
                    UserID = Signup.UserId,
                    User_Name = Signup.Name,
                    User_Email = Signup.Email,
                    User_Password = Signup.Password,
                    User_Role = Signup.Role,
                };
                return user;
            }
            catch 
            {
                return null;
            }
        }
        public static SignUpCustomModel BindsignUpToCustomeSignUp(User user )
        {
            try
            {
                SignUpCustomModel Signup = new SignUpCustomModel()
                {
                    Name = user.User_Name,
                    Email = user.User_Email,
                    Password = user.User_Password,
                    Role = user.User_Role,
                    UserId = user.UserID
                };
                return Signup;
            }
            catch
            {
                return null;
            }
        }

        public static string getEnumString(int Role)
        {
            try
            {
                return Enum.GetName(typeof(RoleType), Role);
            }
            catch 
            {
                return null;
            }
        }
    }
}
