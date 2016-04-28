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
using System.Text;

namespace ActiveLearning.Web.Controllers
{
    public class AdminController : BaseController
    {
        private IManagerFactoryBase<ICourseManager> _CourseManagerfactory { get; set; }
        private IManagerFactoryBase<IUserManager> _UserManagerfactory { get; set; }

        public AdminController()
        {
            _CourseManagerfactory = new CourseManager();
            _UserManagerfactory = new UserManager();
        }

        public AdminController(IManagerFactoryBase<ICourseManager> _courseFactory, IManagerFactoryBase<IUserManager> _userFactory)
        {
            _CourseManagerfactory = _courseFactory;
            _UserManagerfactory = _userFactory;
        }

        // GET: Course
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult Index()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            return View();
        }

        #region Course


        // GET: ManageCourse
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult CreateCourse(Course course)
        {
            try
            {
                string message = string.Empty;
                using (var courseManager = _CourseManagerfactory.Create())
                {
                    if (courseManager.AddCourse(course, out message) == null)
                    {
                        ViewBag.Error = message;
                        return View();
                    }
                }
                ViewBag.Message = "Create Succesfully!";

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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult DeleteCou(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deleteCourse = new CourseManager())
                {
                    if (deleteCourse == null)
                    {
                        return HttpNotFound();
                    }
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

        // GET: ManageCourse/EditCourse/6
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult EditCourse(int id)
        {
            string message = string.Empty;
            using (var getCourse = new CourseManager())
            {
                Course course = getCourse.GetCourseByCourseSid(id, out message);
                if (course == null)
                {
                    return HttpNotFound();
                }
                TempData["EditCourse"] = course;
                return View(course);
            };
        }

        // POST: ManageCourse/EditCourse/6
        [HttpPost, ActionName("EditCourse")]
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult updateCou(Course course)
        {
            try
            {
                string message = string.Empty;
                var courseToUpdate = TempData["EditCourse"] as Course;
                courseToUpdate.CourseName = course.CourseName;

                using (var updateCourse = new CourseManager())
                {
                    if (updateCourse.UpdateCourse(courseToUpdate, out message))
                    {
                        return RedirectToAction("ManageCourse");
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



        // POST: ManageCourse/Details/6
        #endregion

        #region Instructor

        // GET: ManageInstructor
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
                    if (updateInstructor.UpdateInstructor(instructorToUpdate, out message))
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
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult CreateInstructor()
        {
            return View();
        }


        // POST: ManageInstructor/CreateInstructor
        [HttpPost]
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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

        // GET: ManageInstructor/InstructorDetails
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult InstructorDetails(int id)
        {
            string message = string.Empty;
            using (var getInstructor = new UserManager())
            {
                var instructor = getInstructor.GetInstructorByInstructorSid(id, out message);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            };

        }

        // POST: ManageInstructor/ActivateInstructor
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult ActivateInstructor(int id)
        {
            try
            {
                string message = string.Empty;
                using (var activateInstructor = new UserManager())
                {
                    //if (student == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    var instructor = activateInstructor.GetInstructorByInstructorSid(id, out message);
                    if (activateInstructor.ActivateInstructor(instructor, out message))
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

        // POST: ManageStudent/DeactivateInstructor
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult DeactivateInstructor(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deactivateInstructor = new UserManager())
                {
                    //if (student == null)
                    //{
                    //    return HttpNotFound();
                    //}
                    var instructor = deactivateInstructor.GetInstructorByInstructorSid(id, out message);
                    if (deactivateInstructor.DeactivateInstructor(instructor, out message))
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
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult ManageStudent()
        {
            string message = string.Empty;
            using (var userManager = _UserManagerfactory.Create())
            {

                List<Student> listStudent = (List<Student>)userManager.GetAllStudent(out message);
                if (listStudent == null || listStudent.Count == 0)
                {
                    ViewBag.Message = "The List is empty !";
                }

                return View(listStudent);
            }

        }

        // GET: ManageStudent/StudentDetails
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult CreateStudent()
        {
            return View();

        }

        // POST: ManageStudent/CreateStudent
        [HttpPost]
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult CreateStudent(Student student)
        {

            try
            {
                string message = string.Empty;
                using (var userManager = _UserManagerfactory.Create())
                {
                    if (userManager.AddStudent(student, out message) == null)
                    {
                        ViewBag.Error = message;
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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
        [CustomAuthorize(Roles = Constants.Admin)]
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


        // POST: ManageStudent/ActivateStudent
        [HttpPost]
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult ActivateStudent(int id)
        {
            try
            {
                string message = string.Empty;
                using (var activateStudent = new UserManager())
                {
                    if (activateStudent == null)
                    {
                        return HttpNotFound();
                    }
                    var student = activateStudent.GetStudentByStudentSid(id, out message);
                    if (activateStudent.ActivateStudent(student, out message))
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

        // POST: ManageStudent/DeactivateStudent
        //[HttpPost]
       [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult DeactivateStudent(int id)
        {
            try
            {
                string message = string.Empty;
                using (var deactivateStudent = new UserManager())
                {
                    if (deactivateStudent == null)
                    {
                        return HttpNotFound();
                    }
                    var student = deactivateStudent.GetStudentByStudentSid(id, out message);
                    if (deactivateStudent.DeactivateStudent(student, out message))
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

        #region Enrolment


        // GET: ManageCourse
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult ManageEnrolment(int id)
        {
            string message = string.Empty;
            using (var enrol = new CourseManager())
            {

                var listStudent = enrol.GetAllActiveStudentsWithHasEnrolledIndicatorByCourseSid(id, out message);
                if (listStudent == null)
                {
                    ViewBag.Message = message;
                }
                //var checkedStudentId = enrol.GetEnrolledStudentSidsByCourseSid(id, out message);
                //TempData["CheckedStudent"] = checkedStudentId;
                TempData["CourseId"] = id;
                TempData["EntrolStudent"] = listStudent.ToList();
                return View(listStudent.ToList());
            }
        }
        
        // POST: ManageCourse/UpdateEnrolment
        //[HttpPost, ActionName("ManageEnrolment")]
        [CustomAuthorize(Roles = Constants.Admin)]
        public ActionResult UpdateEnrolment(IList<Student> student)
        {
            string message = string.Empty;
            var studentEnrol = TempData["EntrolStudent"] as List<Student>;
            int courseId = Convert.ToInt32(TempData["CourseId"]);
            for (int i = 0; i < studentEnrol.Count(); i++)
            {
                studentEnrol[i].HasEnrolled = student[i].HasEnrolled;
            }

            using (var enrolStudent = new CourseManager())
            {
                if (enrolStudent.UpdateStudentsCourseEnrolmentByHasEnrolledIndicator(studentEnrol, courseId, out message))
                {
                    //var enrolList = enrolStudent.GetAllActiveStudentsWithHasEnrolledIndicatorByCourseSid(courseId, out message);  
                    ViewBag.Message = message;
                    return RedirectToAction("ManageEnrolment", new { id = courseId });
                }
                return View();
            }
        }

    }

    #endregion
}
