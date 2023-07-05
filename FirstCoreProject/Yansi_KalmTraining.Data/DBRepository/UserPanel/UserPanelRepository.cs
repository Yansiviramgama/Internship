using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Model.Config;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Data.DBRepository.UserPanel
{
    public class UserPanelRepository : BaseRepository, IUserPanelRepository
    {
        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public UserPanelRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }

        public async Task<UserResponseModel> UserLogin(UserLoginModel login)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserEmail", login.User_Email);
            parameter.Add("@UserPassword", login.User_Password);
            var data = await QueryFirstOrDefaultAsync<UserResponseModel>("Sp_UserLogin", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId);
            parameter.Add("@Token", Token);
            var data = await ExecuteAsync<long>("Sp_UpdateToken",parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> ValidateUserTokenData(int UserId, string jwtToken, DateTime TokenValidDate)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserID", UserId);
            parameter.Add("@Token", jwtToken);
            parameter.Add("@ValidDate", TokenValidDate);
            var data = await QueryFirstOrDefaultAsync<int>("Sp_ValidateToken",parameter,commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> UpdateOTP(int OTP, int Id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserID", Id);
            parameter.Add("@OTP", OTP);
            var data = await ExecuteAsync<int>("SP_UpdateOTP",parameter,commandType:CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> VerifyOTP(int OTP, string Email)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@OTP", OTP);
            parameter.Add("@EMAIL", Email);
            var data = await QueryFirstOrDefaultAsync<int>("SP_VerifyOTP", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> UpdatePassword(string Password, string Email)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@EMAIL", Email);
            parameter.Add("@NEWPASSWORD", Password);
            var data = await QueryFirstOrDefaultAsync<int>("SP_Update_Password", parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<UserResponseModel> GetUserDatabyEmail(string Email)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Email", Email);
            var data = await QueryFirstOrDefaultAsync<UserResponseModel>("Sp_GetUserDataByEmail",parameter,commandType: CommandType.StoredProcedure);
            return data;
        }
        #endregion
    }
}
