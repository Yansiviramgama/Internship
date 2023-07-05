using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Services.Company;
using Yansi_KalmTraining.Services.UserLogin;

namespace Yansi_KalmTraining.Services
{
    public class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var ServiceDictionary = new Dictionary<Type, Type>()
            {
                {typeof(IUserPanelServices ),typeof(UserPanelServices) },
                { typeof(ICompanyDetailsServices), typeof(CompanyDetailsServices) }
            };
            return ServiceDictionary;
        }
    }
}
