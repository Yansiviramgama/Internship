using Newtonsoft.Json;
using SchoolManagement_331.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SchoolManagement_331.WebApi2.Controllers
{

   [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class StateController : ApiController
    {
        public IHttpActionResult GetStates()
        {
            List<State> states;
            using(SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities())
            {
                states = db.State.ToList();
            }
            string data = JsonConvert.SerializeObject(states);
            return Ok(data);
        }
    }
}
