using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
    public interface IFormDetailsInterface
    {
        bool AddFormDetails(FormDetailsCustomeModel formDetailsCustome, int? id);
        Form_Data GetUserById(int? id);
        //List<FormDetailsCustomeModel> GetUsers();
        List<Form_Data> GetUsers();
        int DeleteUser(int? id);
    }
}
