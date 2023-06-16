using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Repository.Services
{
    public class ItemServices : IItemInterface
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        public List<TableCouponCodeMaster> GetCoupon()
        {
            return db.TableCouponCodeMaster.ToList();
        }

        public TableCouponCodeMaster GetCouponbyId(int ? id)
        {
            var table = db.TableCouponCodeMaster.Where(x => x.CouponICoded == id).FirstOrDefault();
            return table;
        }

        public TableItemMaster GetItembyId(int ? id)
        {
            var data = db.TableItemMaster.Where(x => x.ItemID == id).FirstOrDefault();
            return data;
        }

        public List<TableItemMaster> GetItems()
        {
            List<TableItemMaster> list = db.TableItemMaster.ToList();
            return list;
        }
    }
}
