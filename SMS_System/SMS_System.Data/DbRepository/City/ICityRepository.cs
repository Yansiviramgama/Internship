using SMS_System.Model.ViewModel;

namespace SMS_System.Data.DbRepository.City
{
    public interface ICityRepository
    {
        Task<List<CityModel>> GetAllCity();
        Task<int> AddEditCity(CityModel City);
        Task<CityModel> GetCityById(int id);
        Task<int> DeleteCity(int id);
    }
}
