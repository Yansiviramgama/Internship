using SchoolManagement_331.Helper.SignUpHelper;
using SchoolManagement_331.Models.Context;
using SchoolManagement_331.Models.CustomModels;
using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement_331.Controllers
{
    [Validate]
    public class UserProfileController : Controller
    {
        SchoolManagement_YV331Entities db = new SchoolManagement_YV331Entities();     
        public IUserPanelInterfacecs userPanel;
        public UserProfileController(IUserPanelInterfacecs interfacecs)
        {
            userPanel = interfacecs;
        }
        // GET: UserProfile
        public ActionResult AccountDetails()
        {
            try
            {
                
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult EditProfile()
        {
            try
            {
                if(Session["UserEmail"] != null)
                {
                    User userdetail = userPanel.getUserbyEmail(Session["UserEmail"].ToString());
                    SignUpCustomModel signUpCustom = SignUpHelper.BindsignUpToCustomeSignUp(userdetail);
                    return View(signUpCustom);
                }
                else
                {
                    return View();
                }
              
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditProfile(ImageCustomeModel ImgData)
        {
            try
            {
                
                string FileName = Path.GetFileNameWithoutExtension(ImgData.IMAGEPATH.FileName);

                string FileExtension = Path.GetExtension(ImgData.IMAGEPATH.FileName);

                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                ImgData.Image = Server.MapPath(("~/Images/") + FileName);

                ImgData.IMAGEPATH.SaveAs(ImgData.Image);

                Session["ImagePath"] = FileName;
                SessionData.ImagePath = FileName;

                ImageTable iMG = new ImageTable()
                {
                    ImageId = ImgData.ImageId,                  
                    ImageTitle = ImgData.ImageTitle,
                    image = FileName,
                    UserId = (int)Session["UserID"]
                };
                db.ImageTable.Add(iMG);
                db.SaveChanges();
                
                return RedirectToAction("AccountDetails", "UserProfile");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}