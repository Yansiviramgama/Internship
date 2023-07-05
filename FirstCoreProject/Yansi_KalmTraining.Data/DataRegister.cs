using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Data.DBRepository.Company;
using Yansi_KalmTraining.Data.DBRepository.UserPanel;

namespace Yansi_KalmTraining.Data
{
    public class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IUserPanelRepository),typeof(UserPanelRepository)},
                {typeof(ICompanyDetailsRepository),typeof(CompanyDetailsRepository) }
            };
            return dictionary;
        }
    }
}
