using SMS_System.Services.Country;
using SMS_System.Services.JWTauthentication;
using SMS_System.Services.State;
using SMS_System.Services.UserLogin;

namespace SMS_System.Services
{
    public class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var ServiceDictionary = new Dictionary<Type, Type>()
            {
                {typeof(IUserLoginServices ),typeof(UserLoginServices) },
                {typeof(ICountryServices),typeof(CountryServices) },
                {typeof(IStateServices),typeof(StateServices) },
                //{typeof(IJWTAuthenticationServices),typeof(JWTAuthenticationServices) }
            };
            return ServiceDictionary;
        }
    }
}
