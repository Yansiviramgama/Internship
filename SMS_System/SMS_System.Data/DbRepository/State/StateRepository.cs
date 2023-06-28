using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMS_System.Common.Helpers;
using SMS_System.Model.Config;
using SMS_System.Model.ViewModel;
using System.Data;

namespace SMS_System.Data.DbRepository.State
{
    public class StateRepository :BaseRepository, IStateRepository
    {
        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public StateRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }
        public async Task<int> AddEditState(StateModel state)
        {
            if(state.StateId == 0)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID", null);
                parameter.Add("@StateName", state.StateName);
                parameter.Add("@CountryID", state.CountryId);
                return await ExecuteAsync<int>(StoreProcedure.InsertState, parameter, commandType: CommandType.StoredProcedure);
            }
            else
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID", state.StateId);
                parameter.Add("@StateName", state.StateName);
                parameter.Add("@CountryID", state.CountryId);
                return await ExecuteAsync<int>(StoreProcedure.InsertState, parameter, commandType: CommandType.StoredProcedure);
            }           
        }
        public async Task<int> DeleteState(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            return await ExecuteAsync<int>(StoreProcedure.DeleteState,parameter, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<StateModel>> GetAllState()
        {
            var data = await QueryAsync<StateModel>(StoreProcedure.StateList, commandType: CommandType.StoredProcedure);
           return data.ToList();
        }
        public async Task<List<StateModel>> GetStatebyCountry(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            var data = await QueryAsync<StateModel>(StoreProcedure.GetStateByCountryId,parameter,commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        public async Task<StateModel> GetStateById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            return await QueryFirstOrDefaultAsync<StateModel>(StoreProcedure.GetStateById,parameter, commandType: CommandType.StoredProcedure);
        }
        #endregion
    }
}
