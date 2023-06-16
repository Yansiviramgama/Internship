using SchoolManagement_331.Helper.SignUpHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.IO;
using System.Linq;

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
                var email = db.User.Any(x => x.User_Email == user.Email);
                if(email == false)
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
                else
                {
                    return false;
                }
               
            }
            catch 
            {
                return false;
            }
        }
        public int Login(LoginCustomModel Login)
        {
            try
            {
                var login = db.User.Any(x => x.User_Email == Login.Email && x.User_Password == Login.Password);
                var password = db.User.Any(x => x.User_Password != Login.Password && x.User_Email == Login.Email);
                var email = db.User.Any(x => x.User_Email != Login.Email && x.User_Password == Login.Password);
                if (login)
                {
                    return 0;
                }
                else if (password)
                {
                    return 1;
                }
                else if (email)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            catch 
            {
                return -1;
            }
        }

        public User ForgotPassword(SignUpCustomModel customModel)
        {
            try
            {
                User user = db.User.Where(x => x.User_Email.ToLower() == customModel.Email.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch 
            {
                return null;
            }               
        }

        public User getUserbyEmail(string email)
        {
            try
            {
                User user = db.User.Where(x => x.User_Email.ToLower() == email.ToLower()).FirstOrDefault();
                return user;
            }
            catch 
            {
                return null;
            }
        }

    }
}
