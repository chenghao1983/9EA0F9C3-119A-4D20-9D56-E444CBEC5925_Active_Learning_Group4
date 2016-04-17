using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Web.Controllers
{
    public class HomeController : BaseController
    {

        UserManager userManager = null;
        
        public ActionResult Index()
        {
            using(var userManager=new UserManager())
            {
                //string message = string.Empty;
                //var user = userManager.IsAuthenticated("Admin1", "1234", out message);
                return View();
            }
            
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
            using(var userManager = new UserManager())
            {
                string message = string.Empty;

                BaseController baseController = new BaseController();
                if (userManager.IsAuthenticated(user.Username, user.Password, out message) != null)
                {
                    LogUserIn(user);
                    switch (baseController.GetLoginUserRole())
                    {
                        case "Admin":
                            Redirect("~/Admin");
                            break;
                        case "Instructor":
                            Redirect("~/Instructor");
                            break;
                        case "Student":
                            Redirect("~/Student");
                            break;

                    }
                   
                }

                ViewBag.Message = message;
                return View();
            }

        }
        
        #endregion
    }
}