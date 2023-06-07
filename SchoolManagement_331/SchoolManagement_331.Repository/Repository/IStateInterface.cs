using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
   public interface IStateInterface
    {
        bool AddState(StateCustomeModel state, int? id);
        State GetStateById(int? id);
        List<State> GetStates();
        int DeleteState(int? id);
        List<State> GetStatesbyCountry(int? id);
    }
}
