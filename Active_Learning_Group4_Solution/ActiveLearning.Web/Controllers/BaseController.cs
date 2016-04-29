﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.DB;
using System.Reflection;
using ActiveLearning.Web.Filter;
using log4net;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Property
        public static string UserSessionParam = "LoginUser";
        protected const string LF = "\r\n";
        private const string SEPARATOR = "---------------------------------------------------------------------------------------------------------------";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Message
        public string TempDataMessage
        {
            get { return TempData["Message"] == null ? null : TempData["Message"].ToString(); }
            set { TempData["Message"] = value; }
        }
        public string TempDataError
        {
            get { return TempData["Error"] == null ? null : TempData["Error"].ToString(); }
            set { TempData["Error"] = value; }
        }
        public string ViewBagMessage
        {
            get { return ViewBag.Message; }
            set { ViewBag.Message = value; }
        }
        public string ViewBagError
        {
            get { return ViewBag.Error; }
            set { ViewBag.Error = value; }
        }
        public void GetErrorAneMessage()
        {
            ViewBagMessage = TempDataMessage;
            ViewBagError = TempDataError;
        }
        public void SetError(string error)
        {
            TempDataError = error;
            ViewBagError = error;
        }
        public void SetMessage(string message)
        {
            TempDataMessage = message;
            ViewBagMessage = message;
        }
        #endregion

        #region Redirection
        public ActionResult RedirectToError(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
        public ActionResult RedirectToLogin()
        {
            LogUserOut();
            return Redirect("~/home");
        }
        public ActionResult RedirectToHome()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            switch (GetLoginUserRole())
            {
                case Constants.User_Role_Student_Code:
                    return Redirect("~/student");
                    break;
                case Constants.User_Role_Instructor_Code:
                    return Redirect("~/instructor");
                    break;
                case Constants.User_Role_Admin_Code:
                    return Redirect("~/admin");
                    break;
                default:
                    return RedirectToLogin();
                    break;
            }
        }
        public ActionResult RedirectToPreviousURL()
        {
            if (Request.UrlReferrer != null && !string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                return new RedirectResult(Request.UrlReferrer.ToString());
            }
            return RedirectToError("Unknow error");
        }
        #endregion

        #region Access
        public bool HasAccessToCourse(int courseSid, out string message)
        {
            using (var userManager = new UserManager())
            {
                return userManager.HasAccessToCourse(GetLoginUser(), courseSid, out message);
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
            HttpContext.Request.Cookies.Clear();
            HttpContext.Response.Cookies.Clear();
            TempData.Clear();
            Session.Clear();
        }

        #endregion

        #region log
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

        #endregion
    }
}