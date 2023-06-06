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
            catch (Exception e)
            {
                throw e;
            }
        }

        public int DeleteState(int? id)
        {
            return db.Sp_Delete_State(id);
        }
     

        
        public State GetStateById(int? id)
        {
            return db.State.Where(x => x.StateID == id).FirstOrDefault();
        }

        public List<State> GetStates()
        {
            return db.State.ToList();
        }
    }
}
