using SMS_System.Data.DbRepository.State;
using SMS_System.Model.ViewModel;

namespace SMS_System.Services.State
{
    public class StateServices : IStateServices
    {
        public IStateRepository _StateRepository;
        public StateServices(IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
        }
        public async Task<int> AddEditState(StateModel state)
        {
            return await _StateRepository.AddEditState(state);
        }

        public async Task<int> DeleteState(int id)
        {
            return await _StateRepository.DeleteState(id);
        }

        public async Task<List<StateModel>> GetAllState()
        {
           return await _StateRepository.GetAllState();
        }

        public async Task<List<StateModel>> GetStatebyCountry(int id)
        {
            return await _StateRepository.GetStatebyCountry(id);
        }

        public async Task<StateModel> GetStateById(int id)
        {
            return await _StateRepository.GetStateById(id);
        }
    }
}
