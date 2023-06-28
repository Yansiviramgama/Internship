using SMS_System.Model.ViewModel;

namespace SMS_System.Services.UserLogin
{
    public interface IUserLoginServices
    {
        Task<UserDataModel> LoginDetails(LoginCustomModel login);
        Task<int> RegisterUser(UserDataModel user);
        Task<int> ValidateUserTokenData(int UserId, string jwtToken, DateTime TokenValidDate);
        Task<long> UpdateLoginToken(string Token, long UserId);
        Task<UserDataModel> CheckUser(UserDataModel userinfo);
        Task<ForgotDetails> GetUserDetailsByEmail(ForgotDetails user);
    }
}
