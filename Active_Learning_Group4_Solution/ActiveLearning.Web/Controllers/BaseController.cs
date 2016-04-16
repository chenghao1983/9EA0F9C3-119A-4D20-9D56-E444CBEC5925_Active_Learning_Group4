using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.DB;

namespace ActiveLearning.Web.Controllers
{
    public class BaseController : Controller
    {
        public static string UserSessionParam = "LoginUser";


        public BaseController()
        {

        }

        public RedirectResult RedirectToLogin()
        {
            return Redirect("~/home");
        }


        //log error
        public void ErrorLog(string error)
        {


        }

        //log warning
        public void WarningLog(string warning)
        {

        }

        //log information
        public void InfoLog(string info)
        {

        }

        public bool IsUserAuthenticated()
        {

            return false;
        }

        public User GetLoginUser()
        {
            return null;
        }

        public string GetLoginUserType()
        {
            return null;
        }

        public void LogUserIn(User user)
        {
            Session[UserSessionParam] = user;
        }
    }
}