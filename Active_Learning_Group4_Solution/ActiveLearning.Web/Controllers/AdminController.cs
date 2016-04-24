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
using ActiveLearning.Business.Interface;

namespace ActiveLearning.Web.Controllers
{
    public class AdminController : BaseController
    {
        private IManagerFactoryBase<ICourseManager> _CourseManagerfactory { get; set; }

        public AdminController()
        {
            _CourseManagerfactory = new CourseManager();
        }

        public AdminController(IManagerFactoryBase<ICourseManager> factory)
        {
            _CourseManagerfactory = factory;
        }

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
            using (var courseManager = _CourseManagerfactory.Create())
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

        // POST: ManageCourse/CreateCourse
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

        // GET: ManageCourse/DeleteCourse/6
        public ActionResult DeleteCourse(int id)
        {
            string message = string.Empty;
            using (var deleteCourse = new CourseManager())
            {
                Course course = deleteCourse.GetCourseByCourseSid(id, out message);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            };

        }


        // POST: ManageCourse/DeleteCourse/6
        [HttpPost, ActionName("DeleteCourse")]
        public ActionResult DeleteCou(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deleteCourse = new CourseManager())
                {
                    //if (course == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    var course = deleteCourse.GetCourseByCourseSid(id, out message);
                    if (deleteCourse.DeleteCourse(course, out message))
                    {
                        return RedirectToAction("ManageCourse");
                    }
                    return View(course);
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


        // POST: ManageCourse/Details/6
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
                TempData["EditInstructor"] = instructor;
                return View(instructor);
            };
        }

        // POST: ManageInstructor/EditInstructor/6
        [HttpPost, ActionName("EditInstructor")]
        public ActionResult updateIns(Instructor instructor)
        {
            try
            {
                string message = string.Empty;
                var instructorToUpdate = TempData["EditInstructor"] as Instructor;
                instructorToUpdate.User.Username = instructor.User.Username;
                instructorToUpdate.User.Password = instructor.User.Password;
                instructorToUpdate.User.FullName = instructor.User.FullName;
                instructorToUpdate.Qualification = instructor.Qualification;

                using (var updateInstructor = new UserManager())
                {
                    if (updateInstructor.UpdateInstructor(instructorToUpdate,out message))
                    {
                        return RedirectToAction("ManageInstructor");
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
                TempData["EditStudent"] = student;
                return View(student);
            };
        }

        // POST: ManageStudent/EditStudent/6
        [HttpPost, ActionName("EditStudent")]
        public ActionResult updateStu(Student student)
        {
            try
            {
                string message = string.Empty;
                var studentToUpdate = TempData["EditStudent"] as Student;
                studentToUpdate.User.Username = student.User.Username;
                studentToUpdate.User.Password = student.User.Password;
                studentToUpdate.User.FullName = student.User.FullName;

                studentToUpdate.BatchNo = student.BatchNo;
                using (var updateStudent = new UserManager())
                {
                    if (updateStudent.UpdateStudent(studentToUpdate, out message))
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
