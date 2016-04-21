using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()

        {
            return Redirect("~/Home/login");
        }

        public ActionResult About()
        {

            //User user = new DB.User();
            //user.Username = "superman";
            //user.Password = "police";

            //IUserManager userMaanager = new UserManager();
            ////userMaanager.AddNewInstructor(user);
            //userMaanager.isAuthenticated(user);

            //CourseManager asd = new CourseManager();
            //asd.RemoveStudentFromCourse(1, 15);







            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #region login
        // GET: /Home/Login
        [HttpGet]
        public ActionResult Login()
        {
            //Session.Clear();
            return View();
        }

        //POST: /Home/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            ActionResult result = View();
            using (var userManager = new UserManager())
            {
                string message = string.Empty;
                var authenticatedUser = userManager.IsAuthenticated(user.Username, user.Password, out message);
                if (authenticatedUser != null)
                {
                    LogUserIn(authenticatedUser);
                    switch (GetLoginUserRole())
                    {
                        case Constants.User_Role_Admin_Code:
                            result = Redirect("~/Admin");
                            break;
                        case Constants.User_Role_Instructor_Code:
                            result = Redirect("~/Instructor");
                            break;
                        case Constants.User_Role_Student_Code:
                            result = Redirect("~/Student");
                            break;
                    }
                }
                ViewBag.Message = message;
                return result;
            }
        }
        #endregion


        // POST: /Home/LogOff
        public ActionResult LogOff()
        {
            LogUserOut();
            return RedirectToLogin();
        }
    }
}