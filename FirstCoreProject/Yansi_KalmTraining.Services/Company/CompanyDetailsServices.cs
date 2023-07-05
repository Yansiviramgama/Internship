using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Data.DBRepository.Company;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Services.Company
{
    public class CompanyDetailsServices : ICompanyDetailsServices
    {
        public ICompanyDetailsRepository _companyDetailsRepository;
        public CompanyDetailsServices(ICompanyDetailsRepository companyDetailsRepository)
        {
            _companyDetailsRepository = companyDetailsRepository;
        }

        public async Task<int> AddEditCompanyDetail(CompanyModel company)
        {
            var data = await _companyDetailsRepository.AddEditCompanyDetail(company);
            return data;
        }

        public async Task<int> DeleteCompany(int id)
        {
            return await _companyDetailsRepository.DeleteCompany(id);   
        }

        public Task<List<CompanyModel>> GetAllCompanyDetails()
        {
           var data = _companyDetailsRepository.GetAllCompanyDetails();
            return data;
        }

        public Task<List<CompanyList>> GetAllCompanyList()
        {
            var data = _companyDetailsRepository.GetAllCompanyList();
            return data;
        }

        public Task<List<CountryModel>> GetAllCountryList()
        {
            var data = _companyDetailsRepository.GetAllCountryList();
            return data;
        }

        public async Task<CompanyModel> GetCompanyById(int id)
        {
            var data = await _companyDetailsRepository.GetCompanyById(id);
            return data;
        }

        public async Task<List<CompanyModel>> GetEmployeeDataTable(string sortColumn, string sortDirection, int OffsetValue, int PagingSize, string searchby)
        {
            var data = await _companyDetailsRepository.GetEmployeeDataTable(sortColumn,sortDirection, OffsetValue, PagingSize, searchby);
            return data;
        }
    }
}
