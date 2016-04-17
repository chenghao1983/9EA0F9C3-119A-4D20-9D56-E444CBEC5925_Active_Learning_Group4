using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using System.Security.Principal;
using System.Security.Claims;
using ActiveLearning.Web.Filter;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {

            return View();
        }

        #region Course

        public ActionResult CreateCourse()
        {
            User user = new User();
            user.Role = Constants.User_Role_Student_Code;
            user.Username = "Joe";

            LogUserIn(user);

            ViewBag.Title = "Create Course";
            return View();
        }

        [CustomAuthorize(Roles = Constants.User_Role_Admin_Code + "," + Constants.User_Role_Instructor_Code)]
        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
            using (var courseManager = new CourseManager())
            {
                courseManager.AddCourse(course);
            }
            ViewBag.Message = "Course Created !";
            return View();
        }


        #endregion


        #region Instructor

        public ActionResult CreateInstructor()
        {
            string message = string.Empty;
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            using (var userManager = new UserManager())
            {

                userManager.GetAllActiveInstructor(out message);
            }

            ViewBag.Title = "Create Instructor";
            return View();
        }



        [HttpPost]
        public ActionResult CreateInstructor(Instructor instructor)
        {
            // HttpContext.User.Identity
            string message = string.Empty;

            using (var userManager = new UserManager())
            {
                userManager.AddInstructor(instructor, out message);
            }
            ViewBag.Message = "Instructor Created !";
            return View();
        }

        #endregion



        #region Student

        #endregion
    }
}