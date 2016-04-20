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
            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                courseManager.AddCourse(course, out message);
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


        // GET: ManageStudent
        public ActionResult ManageStudent()
        {
            string message = string.Empty;
            using (var userManager = new UserManager())
            {

                List<Student> listStudent = (List<Student>)userManager.GetAllStudent(out message);
                return View(listStudent);
            }

        }

        // GET: ManageStudent/CreateStudent
        public ActionResult CreateStudent()
        {
            return View();

        }

        // POST: ManageStudent/CreateStudent
        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {

            try
            {
                string message = string.Empty;
                using (var userManager = new UserManager())
                {
                    if (userManager.AddStudent(student, out message) == null)
                    {
                        ViewBag.Message = message;
                        return View();
                    }
                }
                ViewBag.Message = "Student Created !";

                return RedirectToAction("ManageStudent");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }


        // GET: ManageStudent/EditStudent/6
        public ActionResult EditStudent(int id)
        {
            string message = string.Empty;
            using (var getStudent = new UserManager())
            {
                Student student = getStudent.GetStudentByStudentSid(id, out message);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            };
        }

        // POST: ManageStudent/EditStudent/6
        [HttpPost,ActionName("EditStudent")]
        public ActionResult update(Student student)
        {
            try
            {
                string message = string.Empty;
                using (var updateStudent=new UserManager())
                {
                    if(updateStudent.UpdateStudent(student,out message))
                    {
                        return RedirectToAction("ManageStudent");
                    }
                    ViewBag.Message = message;
                    return View();
                }
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }
        }

        #endregion

    }
}
