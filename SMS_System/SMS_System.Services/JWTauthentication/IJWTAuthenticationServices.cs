using SMS_System.Model.ViewModel;

namespace SMS_System.Services.JWTauthentication
{
    public interface IJWTAuthenticationServices
    {
        string GenerateToken(string EmailAddress,string UserId, string SecretKey, double JWT_Validity_Mins);
        AccessTokenModel GenerateTokenModel(UserTokenModel userToken, string JWT_Secret, int JWT_Validity_Mins);
        UserTokenModel GetUserTokenData(string jwtToken);
    }
}
