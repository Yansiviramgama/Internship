using SMS_System.Model.common;
using SMS_System.Model.ViewModel;

namespace SMS_System.Services.Country
{
    public interface ICountryServices
    {
        Task<List<CountryModel>> GetAllCountry();
        Task<int> AddEditCountry(CountryModel country);
        Task<CountryModel> GetCountryById(int id);
        Task<int> DeleteCountry(int id);
        Task<List<CountryModel>> CountryPerPage(Pagination pagination);
        Task<int> CountryCount();
    }
}
