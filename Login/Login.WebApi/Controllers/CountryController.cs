using Login.Helper.Helpers;
using Login.Model.CustomModel;
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
    public class CountryController : ApiController
    {
        YansiViramgama325Entities db = new YansiViramgama325Entities();
        public CountryController()
        {

        }
        public async Task<IHttpActionResult> GetAllCountry()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Stu_Country> _Countries = await db.Stu_Country.ToListAsync();
            return Ok(_Countries);
        }
        public IHttpActionResult PostAddEditCountry(CountryCustomModel Country)
        {
            try
            {
                if (Country.CountryID == 0)
                {
                    var countryData = db.Stu_Country.Any(x => x.CountryName.ToLower() == Country.CountryName.ToLower());
                    if (countryData == false)
                    {
                        db.Sp_AddEdit_Country(null, Country.CountryName);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    db.Sp_AddEdit_Country(Country.CountryID, Country.CountryName);
                    return Ok();
                }


            }
            catch
            {
                return NotFound();
            }
        }
        public async Task<IHttpActionResult> GetCountryById(int ? id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                Stu_Country data = await db.Stu_Country.Where(x => x.ID == id).FirstOrDefaultAsync();

                return Ok(data);
            }
            catch
            {
                return null;
            }
        }
        public IHttpActionResult DeleteCountry(int?id)
        {
            try
            {
                if (id != null)
                {
                    db.Sp_Delete_Country(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
//bool AddCountry(CountryCustomModel Country, int? id);
//Country GetCountryById(int? id);