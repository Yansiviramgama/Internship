using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Model.ViewModel
{
    public class UserProfileModel
    {
        public int USERId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public string? ProfilePic { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Emailid { get; set; }
    }
    public class ProfileUpdateModel
	{
		public int USERId { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime BirthDate { get; set; }
		public string BrithDateStr
		{
			get
			{
				return BirthDate.ToString("yyyy-MM-dd");
			}
			set
			{
				BirthDate = Convert.ToDateTime(value);
			}
		}
		public string? Gender { get; set; }
		public int CountryId { get; set; }
		public int StateId { get; set; }
		public int CityId { get; set; }
		public string? ProfilePic { get; set; }
		public IFormFile? imageFile { get; set; }
		public string? FName { get; set; }
		public string? LName { get; set; }
		public string? Emailid { get; set; }
	}
}
