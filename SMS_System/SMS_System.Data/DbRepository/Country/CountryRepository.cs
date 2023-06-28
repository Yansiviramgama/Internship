using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMS_System.Common.Helpers;
using SMS_System.Model.common;
using SMS_System.Model.Config;
using SMS_System.Model.ViewModel;
using System.Data;

namespace SMS_System.Data.DbRepository.Country
{
	public class CountryRepository : BaseRepository, ICountryRepository
	{
		#region Field
		public IConfiguration _Configuration;
		#endregion
		#region Constructor
		public CountryRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
		{
			_Configuration = configuration;
		}
		public async Task<int> AddEditCountry(CountryModel country)
		{
			if (country.CountryId == 0)
			{
				var parameter = new DynamicParameters();
				parameter.Add("@ID", null);
				parameter.Add("@CountryName", country.CountryName);
				return await ExecuteAsync<int>(StoreProcedure.InsertCountry, parameter, commandType: CommandType.StoredProcedure);
			}
			else
			{
				var parameter = new DynamicParameters();
				parameter.Add("@ID", country.CountryId);
				parameter.Add("@CountryName", country.CountryName);
				return await ExecuteAsync<int>(StoreProcedure.InsertCountry, parameter, commandType: CommandType.StoredProcedure);
			}
		}
		public async Task<int> CountryCount()
		{
			return await ExecuteAsync<int>(StoreProcedure.countrycount, commandType: CommandType.StoredProcedure);
		}
		public async Task<List<CountryModel>> CountryPerPage(Pagination pagination)
		{
			var parameter = new DynamicParameters();
			parameter.Add("@PageNumber", pagination.pagenumber);
			parameter.Add("@PageSize", pagination.pagesize);
			var data = await QueryAsync<CountryModel>(StoreProcedure.Pagination, parameter, commandType: CommandType.StoredProcedure);
			return data.ToList();
		}
		public async Task<int> DeleteCountry(int id)
		{
			var Parameter = new DynamicParameters();
			Parameter.Add("@ID", id);
			var data =  await ExecuteAsync<int>(StoreProcedure.DeleteCountry, Parameter, commandType: CommandType.StoredProcedure);
			return data;
		}
		#endregion
		public async Task<List<CountryModel>> GetAllCountry()
		{
			var Data = await QueryAsync<CountryModel>(StoreProcedure.CountryList, commandType: CommandType.StoredProcedure);
			return Data.ToList();
		}

		public async Task<CountryModel> GetCountryById(int id)
		{
			var parameter = new DynamicParameters();
			parameter.Add("@ID", id);
			return await QueryFirstOrDefaultAsync<CountryModel>(StoreProcedure.GetCountryById, parameter, commandType:CommandType.StoredProcedure);
		}
	}
}
