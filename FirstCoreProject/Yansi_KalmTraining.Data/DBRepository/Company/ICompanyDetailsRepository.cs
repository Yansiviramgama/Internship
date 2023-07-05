using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Data.DBRepository.Company
{
    public interface ICompanyDetailsRepository
    {
        Task<List<CountryModel>> GetAllCountryList();
        Task<List<CompanyList>> GetAllCompanyList();
        Task<List<CompanyModel>> GetAllCompanyDetails();
        Task<int> AddEditCompanyDetail(CompanyModel company);
        Task<CompanyModel> GetCompanyById(int id);
        Task<int> DeleteCompany(int id);
        Task<List<CompanyModel>> GetEmployeeDataTable(string sortColumn, string sortDirection, int OffsetValue, int PagingSize, string searchby);

        //Task<int> AddEditImage(ImageModel image); 
    }
}
