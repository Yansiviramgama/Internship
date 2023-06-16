using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
   public interface IUserPanelInterfacecs
    {
        bool SignUp(SignUpCustomModel user);

        int Login(LoginCustomModel Login);
        User ForgotPassword(SignUpCustomModel customModel);
        User getUserbyEmail(string email);
       
        
    }
}
