using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement_331.SessionHelper
{
    public class SessionData
    {
        public static string UserEmail
        {
            get
            {
                return HttpContext.Current.Session["UserEmail"] == null ? null : (string)HttpContext.Current.Session["UserEmail"];
            
            }
            set
            {
                HttpContext.Current.Session["UserEmail"] = value;
            }
        }
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] == null ? null : (string)HttpContext.Current.Session["UserName"];

            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
        public static int UserID
        {
            get
            {
                return HttpContext.Current.Session["UserID"] == null ? 0 : (int)HttpContext.Current.Session["UserID"];

            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }
        public static string UserPassword
        {
            get
            {
                return HttpContext.Current.Session["UserPassword"] == null ? null : (string)HttpContext.Current.Session["UserPassword"];

            }
            set
            {
                HttpContext.Current.Session["UserPassword"] = value;
            }
        }
        public static string UserRole
        {
            get
            {
                return HttpContext.Current.Session["UserRole"] == null ? null : (string)HttpContext.Current.Session["UserRole"];

            }
            set
            {
                HttpContext.Current.Session["UserRole"] = value;
            }
        }
        public static string ImagePath
        {
            get
            {
                return HttpContext.Current.Session["ImagePath"] == null ? null : (string)HttpContext.Current.Session["ImagePath"];

            }
            set
            {
                HttpContext.Current.Session["ImagePath"] = value;
            }
        }
    }
}