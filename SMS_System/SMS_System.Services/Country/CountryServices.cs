using SMS_System.Data.DbRepository.Country;
using SMS_System.Model.common;
using SMS_System.Model.ViewModel;

namespace SMS_System.Services.Country
{
    public class CountryServices : ICountryServices
    {
        private ICountryRepository _CountryRepository;
        public  CountryServices(ICountryRepository countryRepository)
        {
            _CountryRepository = countryRepository;
        }

        public async Task<int> AddEditCountry(CountryModel country)
        {
            return await _CountryRepository.AddEditCountry(country);
        }

        public async Task<int> CountryCount()
        {
            return await _CountryRepository.CountryCount();
        }

        public async Task<List<CountryModel>> CountryPerPage(Pagination pagination)
        {
            return await _CountryRepository.CountryPerPage(pagination);
        }

        public async Task<int> DeleteCountry(int id)
        {
            return await _CountryRepository.DeleteCountry(id);
        }

        public async Task<List<CountryModel>> GetAllCountry()
        {
            return await _CountryRepository.GetAllCountry();
        }

        public async Task<CountryModel> GetCountryById(int id)
        {
           return await _CountryRepository.GetCountryById(id);
        }
    }
}
