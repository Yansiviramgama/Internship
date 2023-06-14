using Login.Helper.Helpers;
using Login.Model.CustomModel;
using Login.Model.DbContext;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public async Task<ActionResult> ShowCountry(int ? page)
        {
            try
            {
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                IEnumerable<Stu_Country> countries = null;
                using ( var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50211/api/");
                    var responseTask = await client.GetAsync("Country");

                    var result = responseTask.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsAsync<IList<Stu_Country>>();
                        countries = readTask;
                    }
                    else
                    {
                        countries = null;
                        ModelState.AddModelError(string.Empty, "Server Error , Please Contact Administrator");
                    }
                    return View(countries.ToPagedList(pageNumber,pageSize));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<ActionResult> AddEditCountry(int ? id)
        {
            try
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    Stu_Country countries = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50211/api/");
                        var responseTask = await client.GetAsync("Country?id=" + id.ToString());

                        var result = responseTask.EnsureSuccessStatusCode();
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = await result.Content.ReadAsAsync<Stu_Country>();
                            countries = readTask;
                        }
                        else
                        {
                            countries = null;
                            ModelState.AddModelError(string.Empty, "Server Error , Please Contact Administrator");
                        }
                        CountryCustomModel customModel = CountryHelper.BindCountryToCustomeCountry(countries);
                        return View(customModel);
                    }
                }

            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public async Task<ActionResult> AddEditCountry(CountryCustomModel country)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50211/api/");
                    var responseTask = await client.PostAsJsonAsync<CountryCustomModel>("Country" ,country);

                    var result = responseTask.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ShowCountry", "Country");
                    }
                    else
                    {                       
                        ModelState.AddModelError(string.Empty, "Server Error , Please Contact Administrator");
                    }                   
                    return View(country);
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }

        }
        public async Task<ActionResult> DeleteCountry(int?id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50211/api/");
                    var responseTask = await client.DeleteAsync("Country?id=" + id.ToString());

                    var result = responseTask.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ShowCountry", "Country");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error , Please Contact Administrator");
                    }
                    return RedirectToAction("Error");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Error");
            }

        }

    }
}