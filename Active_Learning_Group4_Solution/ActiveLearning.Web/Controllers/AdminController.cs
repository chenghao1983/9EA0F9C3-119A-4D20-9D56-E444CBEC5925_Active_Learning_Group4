using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;

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
            ViewBag.Title = "Create Course";
            return View();
        }


        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
           using( var courseManager = new CourseManager())
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
            ViewBag.Title = "Create Instructor";
            return View();
        }


        [HttpPost]
        public ActionResult CreateInstructor(Instructor instructor)
        {
            using (var userManager = new UserManager())
            {
                userManager.AddInstructor(instructor);
            }
            ViewBag.Message = "Instructor Created !";
            return View();
        }

        #endregion



        #region Student

        #endregion
    }
}