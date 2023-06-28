
using SMS_System.Data.DbRepository.Country;
using SMS_System.Data.DbRepository.State;
using SMS_System.Data.DbRepository.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Data
{
    public class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IUserLoginRepository),typeof(UserLoginRepository)},
                {typeof(ICountryRepository),typeof(CountryRepository)},
                {typeof(IStateRepository),typeof(StateRepository)}
            };
            return dictionary;
        }
    }
}
