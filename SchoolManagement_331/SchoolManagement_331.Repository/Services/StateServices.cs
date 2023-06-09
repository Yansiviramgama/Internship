using SchoolManagement_331.Helper.StateHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Services
{
   public class StateServices : IStateInterface
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        StateHelper Helper = new StateHelper();

        public bool AddState(StateCustomeModel state, int? id)
        {
            try
            {
                State main = Helper.BindCustomeStateToState(state);
                if (main != null)
                {
                    if (id == 0)
                    {
                        db.Sp_AddEdit_State(null, state.StateName,state.CountryID);
                        return true;
                    }
                    else
                    {
                        db.Sp_AddEdit_State(state.StateID, state.StateName,state.CountryID);
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        public int DeleteState(int? id)
        {
            try
            {
                return db.Sp_Delete_State(id);
            }
            catch 
            {
                return -1;
            }
        }           
        public State GetStateById(int? id)
        {
            try
            {
                return db.State.Where(x => x.StateID == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<State> GetStates()
        {
            try
            {
                return db.State.ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<State> GetStatesbyCountry(int? id)
        {
            try
            {
                var state = db.State.Where(x => x.CountryID == id).ToList();
                return state;
            }
            catch 
            {
                return null;
            }
        }
    }
}
