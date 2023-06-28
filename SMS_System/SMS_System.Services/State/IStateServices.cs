﻿using SMS_System.Model.ViewModel;

namespace SMS_System.Services.State
{
    public interface IStateServices
    {
        Task<List<StateModel>> GetAllState();
        Task<int> AddEditState(StateModel state);
        Task<StateModel> GetStateById(int id);
        Task<int> DeleteState(int id);
        Task<List<StateModel>> GetStatebyCountry(int id);
    }
}
