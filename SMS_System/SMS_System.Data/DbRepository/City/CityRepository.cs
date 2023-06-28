using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMS_System.Common.Helpers;
using SMS_System.Model.Config;
using SMS_System.Model.ViewModel;
using System.Data;

namespace SMS_System.Data.DbRepository.City
{
    public class CityRepository : BaseRepository, ICityRepository
    {

        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public CityRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }

        public async Task<int> AddEditCity(CityModel City)
        {
            if(City.Id == 0)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID", null);
                parameter.Add("@CityName", City.CityName);
                parameter.Add("@stateID", City.StateId);
                parameter.Add("@CountryID", City.CountryId);
                return await ExecuteAsync<int>(StoreProcedure.InsertCity, parameter, commandType: CommandType.StoredProcedure);
            }
            else
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID", City.Id);
                parameter.Add("@CityName", City.CityName);
                parameter.Add("@stateID", City.StateId);
                parameter.Add("@CountryID", City.CountryId);
                return await ExecuteAsync<int>(StoreProcedure.InsertCity, parameter, commandType: CommandType.StoredProcedure);
            }
            
        }

        public async Task<int> DeleteCity(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            return await ExecuteAsync<int>(StoreProcedure.DeleteCity,parameter,commandType: CommandType.StoredProcedure);
        }

        public async Task<List<CityModel>> GetAllCity()
        {
            var city = await QueryAsync<CityModel>(StoreProcedure.CityList, commandType: CommandType.StoredProcedure);
            return city.ToList();
        }

        public async Task<CityModel> GetCityById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            return await QueryFirstOrDefaultAsync<CityModel>(StoreProcedure.GetCityById,parameter, commandType: CommandType.StoredProcedure);
        }
        #endregion
    }
}
