using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Data.DBRepository.UserPanel
{
    public interface IUserPanelRepository
    {
        Task<UserResponseModel> UserLogin(UserLoginModel login);
        Task<int> ValidateUserTokenData(int UserId, string jwtToken, DateTime TokenValidDate);
        Task<long> UpdateLoginToken(string Token, long UserId);
        Task<UserResponseModel> GetUserDatabyEmail(string Email);
        Task<int> UpdateOTP(int OTP, int Id);

        //To verify OTP
        Task<int> VerifyOTP(int OTP, string Email);

        //To Update Admin Password 
        Task<int> UpdatePassword(string Password, string Email);
    }
}
