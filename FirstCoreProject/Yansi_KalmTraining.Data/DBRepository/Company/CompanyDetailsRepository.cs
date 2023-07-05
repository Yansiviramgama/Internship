using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yansi_KalmTraining.Model.Config;
using Yansi_KalmTraining.Model.Viewmodel;

namespace Yansi_KalmTraining.Data.DBRepository.Company
{
    public class CompanyDetailsRepository :BaseRepository, ICompanyDetailsRepository
    {
        #region Field
        public IConfiguration _Configuration;
        #endregion
        #region Constructor
        public CompanyDetailsRepository(IConfiguration configuration, IOptions<DataConfig> dataconfig) : base(dataconfig, configuration)
        {
            _Configuration = configuration;
        }

        public async Task<int> AddEditCompanyDetail(CompanyModel company)
        {
            if (company.Id == 0)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID",0);
                parameter.Add("@CompanyID",company.CompanyID);
                parameter.Add("@Cityname", company.Cityname);
                parameter.Add("@CountryId", company.CountryID);
                parameter.Add("@PostCode", company.PostCode);
                parameter.Add("@ComapanyLogo", company.Image);
                parameter.Add("@Tool", company.Tool);               
                var data = await ExecuteAsync<int>("Sp_AddEditCompanyDetails", parameter, commandType: CommandType.StoredProcedure);
                return data;
            }
            else
            {
                var parameter = new DynamicParameters();
                parameter.Add("@ID",company.Id);
                parameter.Add("@CompanyID", company.CompanyID);
                parameter.Add("@Cityname", company.Cityname);
                parameter.Add("@CountryId", company.CountryID);
                parameter.Add("@PostCode", company.PostCode);
                parameter.Add("@ComapanyLogo", company.Image);
                parameter.Add("@Tool", company.Tool);
                var data = await ExecuteAsync<int>("Sp_AddEditCompanyDetails", parameter, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
        public async Task<int> DeleteCompany(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            var data = await ExecuteAsync<int>("Sp_DeleteCompany", parameter,commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<List<CompanyModel>> GetAllCompanyDetails()
        {
            var data = await QueryAsync<CompanyModel>("Sp_CompanyDetails", commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public async Task<List<CompanyList>> GetAllCompanyList()
        {
            var data = await QueryAsync<CompanyList>("Sp_AllCompaniesList", commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
        #endregion
        public async Task<List<CountryModel>> GetAllCountryList()
        {
            var data = await QueryAsync<CountryModel>("Sp_AllCountriesList", commandType: CommandType.StoredProcedure);
            var list = data.ToList();
            return list;
        }

        public async Task<CompanyModel> GetCompanyById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var data = await QueryFirstOrDefaultAsync<CompanyModel>("Sp_CompanyDetailsById",parameter, commandType: CommandType.StoredProcedure);
            return data;
        }

        public async Task<List<CompanyModel>> GetEmployeeDataTable(string sortColumn, string sortDirection, int OffsetValue, int PagingSize, string searchby)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@sortColumn", sortColumn);
            parameter.Add("@sortOrder", sortDirection);
            parameter.Add("@OffsetValue", OffsetValue);
            parameter.Add("@PagingSize", PagingSize);
            parameter.Add("@SearchText", searchby);
            var data = await QueryAsync<CompanyModel>("Sp_DataInDataTable", parameter, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
    }
}
