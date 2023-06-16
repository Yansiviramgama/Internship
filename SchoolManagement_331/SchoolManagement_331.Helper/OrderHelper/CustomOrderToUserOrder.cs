using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement_331.Helper.OrderHelper
{
  public  class CustomOrderToUserOrder
    {
       
        public static UserOrder BindCustomOrderToUserOrder(OrderCustomModel order)
        {
            UserOrder user = new UserOrder()
            {
                Order_Date = DateTime.Now,
                UserID = Convert.ToInt32(order.UserId),
                Total_Qty = Convert.ToInt32(order.Total_Qty),
                Amount = Convert.ToDecimal(order.Amt),
                AFTER_GST = Convert.ToDecimal(order.After_GST),
                PROMO = order.Promo,
                TOTAL_PAYABLE = Convert.ToDecimal(order.Total_Payable)
            };
            return user;
            
        }
        public static List<ItemDetails> BindCustomOrderToItem(OrderCustomModel order)
        {
            List<ItemDetails> Item = new List<ItemDetails>();
            order.Items.ForEach(e =>
            {
                ItemDetails item = new ItemDetails()
                {
                    ITEM_NAME = e.ITEM_NAME,
                    ITEM_PRICE = e.ITEM_PRICE,
                    ITEM_QTY = e.ITEM_QTY,
                    ORDERID = e.ORDERID
                };
                Item.Add(item);
            });
            return Item;

        }
    }
}
