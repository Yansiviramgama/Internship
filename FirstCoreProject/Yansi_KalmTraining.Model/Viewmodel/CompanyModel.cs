using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yansi_KalmTraining.Model.Viewmodel
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Cityname { get; set; }
        public string PostCode { get; set; }
        public int FilterTotalCount { get; set; }
        public string ImageTitle { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Image { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public string Tool { get; set; }   
    }
    public class CompanyList
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set;}
    }
    public class CountryModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
    public class DataTableModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string Cityname { get; set; }
        public string PostCode { get; set; }
        public string Tool { get; set; }
    }
    
}
