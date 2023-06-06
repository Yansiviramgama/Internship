using SchoolManagement_331.Helper.SignUpHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Services
{
    public class UserPanelServices : IUserPanelInterfacecs
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        //SignUpHelper signUp = new SignUpHelper();
       
        public bool SignUp(SignUpCustomModel user)
        {
            try
            {
                User main = SignUpHelper.BindCustomesignUpToSignUp(user);
                if (main != null)
                {
                    db.User.Add(main);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Login(LoginCustomModel Login)
        {
            var login = db.User.Any(x => x.User_Email == Login.Email && x.User_Password == Login.Password);
            if (login)
            {
               
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
