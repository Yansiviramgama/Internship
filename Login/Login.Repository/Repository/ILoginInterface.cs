using Login.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Repository.Repository
{
   public interface ILoginInterface
    {
        int StudentLogin(Student_User user);
    }
}
