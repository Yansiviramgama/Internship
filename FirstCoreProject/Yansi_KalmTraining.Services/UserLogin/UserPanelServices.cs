using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Data.DBRepository.UserPanel;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Services.UserLogin
{
    public class UserPanelServices : IUserPanelServices
    {
        public IUserPanelRepository userPanelRepository;
        public UserPanelServices(IUserPanelRepository userPanelRepository)
        {
            this.userPanelRepository = userPanelRepository;
        }

        public async Task<UserResponseModel> GetUserDatabyEmail(string Email)
        {
            var data = await userPanelRepository.GetUserDatabyEmail(Email);
            return data;
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId)
        {
            var data = await userPanelRepository.UpdateLoginToken(Token, UserId);
            return data;
        }

        public async Task<int> UpdateOTP(int OTP, int Id)
        {
            var data = await userPanelRepository.UpdateOTP(OTP, Id);
            return data;
        }

        public async Task<int> UpdatePassword(string Password, string Email)
        {
            var data = await userPanelRepository.UpdatePassword(Password, Email);
            return data;    
        }

        public async Task<UserResponseModel> UserLogin(UserLoginModel login)
        {
            var data = await userPanelRepository.UserLogin(login);
            return data;
        }

        public async Task<int> ValidateUserTokenData(int UserId, string jwtToken, DateTime TokenValidDate)
        {
            var data = await userPanelRepository.ValidateUserTokenData(UserId, jwtToken, TokenValidDate);
            return data;
        }

        public async Task<int> VerifyOTP(int OTP, string Email)
        {
            var data = await userPanelRepository.VerifyOTP(OTP, Email);
            return data;
        }
    }
}
