using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMS_System.Common.Helpers;
using SMS_System.Model.Config;
using SMS_System.Model.ViewModel;
using System.Data;

namespace SMS_System.Data.DbRepository.UserLogin
{
    public class UserLoginRepository :BaseRepository, IUserLoginRepository
    {
        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public UserLoginRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }
        public async Task<UserDataModel> CheckUser(UserDataModel userinfo)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", userinfo.User_Email);
            return await QueryFirstOrDefaultAsync<UserDataModel>(StoreProcedure.CheckUser, param, commandType:CommandType.StoredProcedure);
        }
        public async Task<ForgotDetails> GetUserDetailsByEmail(ForgotDetails user)
        {
            var param = new DynamicParameters();
            param.Add("@UserEmail", user.EMAILID);
            return await QueryFirstOrDefaultAsync<ForgotDetails>(StoreProcedure.GetUserDetailsByEmail, param, commandType:CommandType.StoredProcedure);
        }
        #endregion
        public async Task<UserDataModel> LoginDetails(LoginCustomModel login)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserEmail", login.UserEmail);
            parameter.Add("@Password", login.Password);
            var data =  await QueryFirstOrDefaultAsync<UserDataModel>(StoreProcedure.UserLogin, parameter,commandType:CommandType.StoredProcedure);
            return data;
        }

        public async Task<int> RegisterUser(UserDataModel user)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@UserID", user.UserID);
            parameter.Add("@User_Name", user.User_Name);
            parameter.Add("@User_Email", user.User_Email);
            parameter.Add("@User_Password", user.User_Password);
            return await ExecuteAsync<int>(StoreProcedure.UserInfo,parameter,commandType:CommandType.StoredProcedure);
        }

        public async Task<long> UpdateLoginToken(string Token, long UserId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Token", Token);
                param.Add("@UserId", UserId);
                return await QueryFirstOrDefaultAsync<long>(StoreProcedure.UpdateLoginToken, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> ValidateUserTokenData(long UserId, string jwtToken, DateTime TokenValidDate)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", UserId);
                param.Add("@Token", jwtToken);
                param.Add("@ValidateDate", TokenValidDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                return await QueryFirstOrDefaultAsync<long>(StoreProcedure.ValidateTokenDate, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
