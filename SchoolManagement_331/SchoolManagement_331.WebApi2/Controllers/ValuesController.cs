using SchoolManagement_331.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagement_331.WebApi2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<State> GetStates()
        {
            using (SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities())
            {
                return db.State.ToList();
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
