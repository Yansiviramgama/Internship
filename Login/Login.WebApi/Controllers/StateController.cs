using Login.Helper.Helpers;
using Login.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Login.WebApi.Controllers
{
    public class StateController : ApiController
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        public StateController()
        {

        }
        public async Task<IHttpActionResult> GetAllStates()
        {
           
            List<Stu_State> _States = await db.Stu_State.ToListAsync();
            var stateList = StateHelper.BindCtModelListToList(_States);
           
            return Ok(stateList);

        }
    }
}
