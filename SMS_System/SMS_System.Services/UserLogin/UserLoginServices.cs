using SMS_System.Data.DbRepository.UserLogin;
using SMS_System.Model.ViewModel;

namespace SMS_System.Services.UserLogin
{
    public class UserLoginServices : IUserLoginServices
    {
        #region Fields
        private IUserLoginRepository Services;
        #endregion
        #region constructor
        public UserLoginServices(IUserLoginRepository _Services)
        {
            Services = _Services;   
        }

        public async Task<UserDataModel> CheckUser(UserDataModel userinfo)
        {
            return await Services.CheckUser(userinfo);
        }

        public async Task<ForgotDetails> GetUserDetailsByEmail(ForgotDetails user)
        {
            return await Services.GetUserDetailsByEmail(user);
        }
        #endregion
        public async Task<UserDataModel> LoginDetails(LoginCustomModel login)
        {
            return await Services.LoginDetails(login);
        }

        public async Task<int> RegisterUser(UserDataModel user)
        {
            return await Services.RegisterUser(user);
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId)
        {
            return await Services.UpdateLoginToken(Token, UserId);
        }

        public async Task<int> ValidateUserTokenData(int UserId, string jwtToken, DateTime TokenValidDate)
        {
            return await Services.ValidateUserTokenData(UserId, jwtToken, TokenValidDate);
        }
    }
}
