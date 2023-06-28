using SMS_System.Model.ViewModel;

namespace SMS_System.Data.DbRepository.UserLogin
{
    public interface IUserLoginRepository
    {
        Task<UserDataModel> LoginDetails(LoginCustomModel login);
        Task<int> RegisterUser(UserDataModel user);
        Task<UserDataModel> CheckUser(UserDataModel userinfo);
        Task<ForgotDetails> GetUserDetailsByEmail(ForgotDetails user);
        Task<long> ValidateUserTokenData(long UserId, string jwtToken, DateTime TokenValidDate);   
        Task<long> UpdateLoginToken(string Token, long UserId);
    }
}
