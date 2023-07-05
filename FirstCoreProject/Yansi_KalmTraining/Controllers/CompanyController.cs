using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Yansi_KalmTraining.Model.DataTable;
using Yansi_KalmTraining.Model.Viewmodel;
using Yansi_KalmTraining.Services.Company;

namespace Yansi_KalmTraining.Controllers
{
    public class CompanyController : Controller
    {
         //<summary>Fields</summary>
        private readonly IWebHostEnvironment _hostEnvironment;
        public ICompanyDetailsServices CompanyDetailsService;

        // <summary>Constructor</summary>
        public CompanyController(ICompanyDetailsServices companyDetailsService, IWebHostEnvironment hostEnvironment)
        {
            CompanyDetailsService = companyDetailsService;
            _hostEnvironment = hostEnvironment;
        }

        // <summary>Company List Method</summary>
        // <summary>Add Edit On Company List</summary>
        public async Task<IActionResult> CompanyList(int id)
        {
            try
            {
                if (id == 0)
                {
                    TempData["Image"] = "17004.png";
                    ViewBag.CountryList = new SelectList(await CompanyDetailsService.GetAllCountryList(), "CountryID", "CountryName");
                    ViewBag.CompanyList = new SelectList(await CompanyDetailsService.GetAllCompanyList(), "CompanyID", "CompanyName");
                    return View();
                }
                else
                {
                    ViewBag.CountryList = new SelectList(await CompanyDetailsService.GetAllCountryList(), "CountryID", "CountryName");
                    ViewBag.CompanyList = new SelectList(await CompanyDetailsService.GetAllCompanyList(), "CompanyID", "CompanyName");
                    var data = await CompanyDetailsService.GetCompanyById(id);
                    return View(data);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CompanyList(CompanyModel model, IFormFile file)
        {
            try
            {
                string uniqueFileName = null;  //to contain the filename
                if (file != null)  //handle iformfile
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "CompanyLogo");
                    uniqueFileName = file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                model.Image = uniqueFileName; //fill the image property          
                await CompanyDetailsService.AddEditCompanyDetail(model);
                return RedirectToAction("CompanyDetails");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

 
        // <summary>Company Details List Method</summary>
        // <summary>This Method Bind data for show in datatable</summary>
        [HttpPost]
        public async Task<IActionResult> CompanyDataList()
        {
            var employees = await CompanyDetailsService.GetAllCompanyDetails();
            var count = employees.Count();
            IEnumerable<CompanyModel> employee;
            HttpContext context = HttpContext;
            context.Response.ContentType = "text/plain";         
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the search TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = "CompanyName";
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Get the Data from the Database  
            List<CompanyModel> dt = await CompanyDetailsService.GetEmployeeDataTable(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            Int32 recordTotal = 0;
            List<DataTableModel> CompanyList = new List<DataTableModel>();
            //Binding the Data from datatable to the List  
            if (dt != null)
            {
                for (int i = 0; i < dt.Count(); i++)
                {
                    DataTableModel model = new DataTableModel();
                    model.Id = dt[i].Id;
                    model.Image = dt[i].Image;
                    model.PostCode = dt[i].PostCode;
                    model.CountryName = dt[i].CountryName;
                    model.CompanyName = dt[i].CompanyName;
                    model.Cityname = dt[i].Cityname;
                    model.Tool = dt[i].Tool;
                    CompanyList.Add(model);
                }
                recordTotal = dt.Count() > 0 ? Convert.ToInt32(dt.FirstOrDefault().FilterTotalCount) : 0;
            }
            Int32 recordFiltered = recordTotal;
            DataTableResponse objDataTableResponse = new DataTableResponse()
            {
                draw = ajaxDraw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                data = CompanyList
            };
            return Json(objDataTableResponse);
        }

        // <summary>Company Details Method</summary>
        // <summary>This Method Shows All Comapanies Details</summary>
        public IActionResult CompanyDetails()
        {
            try
            {
                
               // var companies = await CompanyDetailsService.GetAllCompanyDetails();
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        // <summary>About Company Method</summary>
        // <summary>This Method Shows Detail of particular Company</summary>
        public async Task<IActionResult> AboutCompany(int id)
        {
            try
            {
                var data = await CompanyDetailsService.GetCompanyById(id);
                if (data.Image != null)
                {
                    return View(data);
                }
                else
                {
                    TempData["Login"] = "17004.png";
                    return View(data);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }          
        }

        // <summary>Delete Company Details Method</summary>
        public async Task<IActionResult> DeleteCompanyDetails(int ID)
        {
            try
            {
                await CompanyDetailsService.DeleteCompany(ID);
                TempData["Success"] = "Data Deleted";
                return RedirectToAction("CompanyDetails");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
