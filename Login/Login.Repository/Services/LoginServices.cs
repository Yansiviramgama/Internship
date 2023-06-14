using Login.Model.DbContext;
using Login.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Repository.Services
{
    public class LoginServices : ILoginInterface
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        public int StudentLogin(Student_User user)
        {
            var login = db.Student_User.Any(x => x.Email == user.Email && x.Password == user.Password);
            var Email = db.Student_User.Any(x => x.Email == user.Email && x.Password != user.Password);
            var Password = db.Student_User.Any(x => x.Email != user.Email && x.Password == user.Password);
            
            if (login)
            {
                return 0;
            }
            else if (Email)
            {
                return 1;
            }
            else if (Password)
            {
                return 2;
            }
            else
            {
                return 3;
            }

        }
    }
}
