using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.DB;
using System.Reflection;
using ActiveLearning.Web.Filter;
using log4net;

namespace ActiveLearning.Web.Controllers
{
    public class BaseController : Controller
    {
        public static string UserSessionParam = "LoginUser";
        protected const string LF = "\r\n";
        private const string SEPARATOR = "---------------------------------------------------------------------------------------------------------------";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public RedirectResult RedirectToLogin()
        {
            return Redirect("~/home");
        }


        //log error
        public static void ExceptionLog(Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void ExceptionLog(string userLog, Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(userLog);
                sb.Append(LF);
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void InfoLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsInfoEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Error(message);
            }
        }

        public static void DebugLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }

        public static void ExceptionLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }

        public bool IsUserAuthenticated()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return false;

                return false;
            }
            return true;
        }

        public User GetLoginUser()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return null;

                return Session[UserSessionParam] as User;
            }
            return TempData.Peek(UserSessionParam) as User;
        }

        public string GetLoginUserRole()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return null;

                return (Session[UserSessionParam] as User).Role;
            }
            return (TempData.Peek(UserSessionParam) as User).Role;
        }

        public void LogUserIn(User user)
        {
            if (Session != null)
                Session[UserSessionParam] = user;

            if (!TempData.Keys.Contains(UserSessionParam))
            {
                TempData.Add(UserSessionParam, user);
                InfoLog("User: " + user.Username + " logged in");
            }
            TempData.Keep(UserSessionParam);
        }

        public void LogUserOut()
        {
            TempData.Clear();
            Session.Clear();
        }
    }
}