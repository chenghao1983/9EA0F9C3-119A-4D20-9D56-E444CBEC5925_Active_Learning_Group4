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


        // GET: ManageCourse
        public ActionResult ManageCourse()
        {
            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {

                var listCourse = courseManager.GetAllCourses(out message);
                if (listCourse == null || listCourse.Count() == 0)
                {
                    ViewBag.Message = "The List is empty !";
                }

                return View(listCourse);
            }

        }

        // GET: ManageCourse/CreateCourse
        public ActionResult CreateCourse()
        {

            return View();
            //User user = new User();
            //user.Role = Constants.User_Role_Student_Code;
            //user.Username = "Joe";

            //LogUserIn(user);

            //ViewBag.Title = "Create Course";
            //return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
            try
            {
                string message = string.Empty;
                using (var courseManager = new CourseManager())
                {
                    if (courseManager.AddCourse(course, out message) == null)
                    {
                        ViewBag.Message = message;
                        return View();
                    }
                }
                ViewBag.Message = "Course Created !";

                return RedirectToAction("ManageCourse");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(course);

            //string message = string.Empty;
            //using (var courseManager = new CourseManager())
            //{
            //    courseManager.AddCourse(course, out message);
            //}
            //ViewBag.Message = "Course Created !";
            //return View();
        }


        #endregion


        #region Instructor

        // GET: ManageInstructor
        public ActionResult ManageInstructor()
        {
            string message = string.Empty;
            using (var userManager = new UserManager())
            {

                List<Instructor> listInstructor = (List<Instructor>)userManager.GetAllActiveInstructor(out message);
                if (listInstructor == null || listInstructor.Count == 0)
                {
                    ViewBag.Message = "The List is empty !";
                }
                return View(listInstructor);
            }

        }

        // GET: ManageInstructor/EditInstructor/6
        public ActionResult EditInstructor(int id)
        {
            string message = string.Empty;
            using (var getInstructor = new UserManager())
            {
                Instructor instructor = getInstructor.GetInstructorByInstructorSid(id, out message);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            };
        }

        // GET: ManageInstructor/CreateInstructor
        public ActionResult CreateInstructor()
        {
            return View();
            //string message = string.Empty;
            //if (!IsUserAuthenticated())
            //{
            //    return RedirectToLogin();
            //}
            //using (var userManager = new UserManager())
            //{

            //    userManager.GetAllActiveInstructor(out message);
            //}

            //ViewBag.Title = "Create Instructor";
            //return View();
        }


        // POST: ManageInstructor/CreateInstructor
        [HttpPost]
        public ActionResult CreateInstructor(Instructor instructor)
        {

            try
            {
                string message = string.Empty;
                using (var userManager = new UserManager())
                {
                    if (userManager.AddInstructor(instructor, out message) == null)
                    {
                        ViewBag.Message = message;
                        return View();
                    }
                }
                ViewBag.Message = "Instructor Created !";

                return RedirectToAction("ManageInstructor");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(instructor);



            // HttpContext.User.Identity
            //string message = string.Empty;

            //using (var userManager = new UserManager())
            //{
            //    userManager.AddInstructor(instructor, out message);
            //}
            //ViewBag.Message = "Instructor Created !";
            //return View();
        }

        // GET: ManageInstructor/DeleteInstructor/6
        public ActionResult DeleteInstructor(int id)
        {
            string message = string.Empty;
            using (var deleteInstructor = new UserManager())
            {
                Instructor instructor = deleteInstructor.GetInstructorByInstructorSid(id, out message);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            };

        }

        // POST: ManageInstructor/DeleteInstructor/6
        [HttpPost, ActionName("DeleteInstructor")]
        public ActionResult DeleteIns(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deleteInstructor = new UserManager())
                {
                    //if (student == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    Instructor instructor = deleteInstructor.GetInstructorByInstructorSid(id, out message);
                    if (deleteInstructor.DeleteInstructor(instructor, out message))
                    {
                        return RedirectToAction("ManageInstructor");
                    }
                    return View(instructor);
                };
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }

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
                if (listStudent == null || listStudent.Count == 0)
                {
                    ViewBag.Message = "The List is empty !";
                }

                return View(listStudent);
            }

        }

        public ActionResult StudentDetails(int id)
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
        [HttpPost, ActionName("EditStudent")]
        public ActionResult update(Student student)
        {
            try
            {
                string message = string.Empty;
                using (var updateStudent = new UserManager())
                {
                    if (updateStudent.UpdateStudent(student, out message))
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
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }
        }

        // GET: ManageStudent/DeleteStudent/6
        public ActionResult DeleteStudent(int id)
        {
            string message = string.Empty;
            using (var deleteStudent = new UserManager())
            {
                Student student = deleteStudent.GetStudentByStudentSid(id, out message);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            };

        }


        // POST: ManageStudent/DeleteStudent/6
        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult Delete(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deleteStudent = new UserManager())
                {
                    //if (student == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    Student student = deleteStudent.GetStudentByStudentSid(id, out message);
                    if (deleteStudent.DeleteStudent(student, out message))
                    {
                        return RedirectToAction("ManageStudent");
                    }
                    return View(student);
                };
            }
            catch (Exception e)
            {
                if (this.HttpContext.IsDebuggingEnabled)
                {
                    ModelState.AddModelError(string.Empty, e.ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical error happened.");
                }
                return View();
            }

        }
        #endregion

    }
}
