using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMS_System.Model.Config;
using SMS_System.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Data.DbRepository.UserProfile
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public UserProfileRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }

        public Task<ProfileUpdateModel> EditProfile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileModel> GetUserProfileById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProfile(ProfileUpdateModel profileUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
