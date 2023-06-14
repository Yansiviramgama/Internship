
using Login.Model.CustomModel;
using Login.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Helper.Helpers
{
   public class StateHelper
    {
        public static List<StateCustomModel> BindCtModelListToList(List<Stu_State> _States)
        {
            try
            {
                List<StateCustomModel> stateList = new List<StateCustomModel>();
                foreach (var item in _States)
                {
                    StateCustomModel stateModel = new StateCustomModel();
                    stateModel.StateID = item.ID;
                    stateModel.StateName = item.StateName;
                    stateModel.CountryID = (int)item.CountryID;
                    stateModel.CountryName = item.Stu_Country.CountryName;
                    stateList.Add(stateModel);
                }
                return stateList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
