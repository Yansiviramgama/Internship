
using Newtonsoft.Json;
using SchoolManagement_331.Helper.OrderHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
    
    public class OrderController : Controller
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();
        IItemInterface itemInterface;
        public OrderController(IItemInterface item)
        {
            itemInterface = item;
        }
        // GET: Order
        public ActionResult Order()
        {
            ViewBag.ItemList = new SelectList(itemInterface.GetItems(),"ItemId","Item_Name");
            return View();
        }
        public ActionResult PlaceOrder(OrderCustomModel customModel)
        {
            CustomOrderToUserOrder.BindCustomOrderToUserOrder(customModel);
           CustomOrderToUserOrder.BindCustomOrderToItem(customModel);
            return View("OrderList");
        }
        public ActionResult OrderList()
        {
            List<GET_ITEM_BY_USERID_Result> Sp = db.GET_ITEM_BY_USERID(Convert.ToInt32(User.Identity.Name)).ToList();
            return View(Sp);
        }

        //public JsonResult GetAllItems()
        //{
        //    var item = itemInterface.GetItems();
        //    var jsonItems = JsonConvert.SerializeObject(item);
        //    return Json(jsonItems, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetItembyId(int id)
        {
            var item = itemInterface.GetItembyId(id);
            var jsonItems = JsonConvert.SerializeObject(item);
            return Json(jsonItems, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCoupon()
        {
            var item = itemInterface.GetCoupon();
            var jsonItems = JsonConvert.SerializeObject(item);
            return Json(jsonItems, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCouponbyId(int id)
        {
            var item = itemInterface.GetCouponbyId(id);
            var jsonItems = JsonConvert.SerializeObject(item);
            return Json(jsonItems, JsonRequestBehavior.AllowGet);
        }
    }
}