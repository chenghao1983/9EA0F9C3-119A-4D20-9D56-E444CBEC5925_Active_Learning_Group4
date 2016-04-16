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
            if (Session[UserSessionParam] == null)
            {
                return false;
            }
            return true;

        }

        public User GetLoginUser()
        {
            if (Session[UserSessionParam] == null)
            {
                return null;
            }
            return Session[UserSessionParam] as User;
        }

        public string GetLoginUserRole()
        {
            if (Session[UserSessionParam] == null)
            {
                return null;
            }
            return (Session[UserSessionParam] as User).Role;
        }

        public void LogUserIn(User user)
        {
            Session[UserSessionParam] = user;
        }
    }
}