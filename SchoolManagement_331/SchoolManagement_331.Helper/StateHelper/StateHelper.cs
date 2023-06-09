using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.StateHelper
{
   public class StateHelper
    {
        public State BindCustomeStateToState(StateCustomeModel State)
        {
            try
            {
                State Statemodel = new State()
                {
                    StateID = State.StateID,
                    StateName = State.StateName,
                    CountryID = State.CountryID
                };
                return Statemodel;
            }
            catch
            {
                return null;
            }

        }
        public StateCustomeModel BindStateToCustomeState(State state)
        {
            try
            {
                StateCustomeModel model = new StateCustomeModel()
                {
                    StateID = state.StateID,
                    StateName = state.StateName,
                    CountryID = state.CountryID
                };
                return model;

            }
            catch 
            {
                return null;
            }
        }
    }
}
