using Login.Model.CustomModel;
using Login.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public async Task<ActionResult> ShowStates()
        {
            try
            {
                IEnumerable<StateCustomModel> states = null;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50211/api/");
                    var responseTask = await client.GetAsync("State");
                    var result = responseTask.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsAsync<IList<StateCustomModel>>();
                        states = readTask;
                    }
                    else
                    {
                        states = null;
                        ModelState.AddModelError(string.Empty, "Server Error , Please Contact Administrator");
                    }
                    return View(states);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        //public async Task<ActionResult> AddEditState()
        //{

        //}
    }
}