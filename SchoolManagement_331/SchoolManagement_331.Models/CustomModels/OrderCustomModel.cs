using SchoolManagement_331.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Models.CustomModels
{
   public class OrderCustomModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Total_Qty { get; set; }
        public string Amt { get; set; }
        public string After_GST { get; set; }
        public string Promo { get; set; }
        public string Total_Payable { get; set; }
        public List<ItemDetails> Items { get; set; }
    }
}
