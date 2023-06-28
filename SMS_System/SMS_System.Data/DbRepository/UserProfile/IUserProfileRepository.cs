
using SMS_System.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Data.DbRepository.UserProfile
{
    public interface IUserProfileRepository
    {
        Task<UserProfileModel> GetUserProfileById(int id);
        Task<int> UpdateProfile(ProfileUpdateModel profileUpdate);
        Task<ProfileUpdateModel> EditProfile(int id);
    }
}
