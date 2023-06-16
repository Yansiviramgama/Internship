using SchoolManagement_331.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Repository
{
   public interface IItemInterface
    {
        List<TableItemMaster> GetItems();
        TableItemMaster GetItembyId(int ? id);

        List<TableCouponCodeMaster> GetCoupon();
        TableCouponCodeMaster GetCouponbyId(int ? id);
    }
}
