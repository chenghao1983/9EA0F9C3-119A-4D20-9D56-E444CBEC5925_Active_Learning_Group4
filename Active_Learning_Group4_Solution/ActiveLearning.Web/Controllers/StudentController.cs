using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ActiveLearning.Business.Interface;
using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using System.Linq;
using ActiveLearning.Business.Common;
using ActiveLearning.Web.Filter;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public ActionResult Index()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            return View();
        }
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public ActionResult Chat(int courseSid)
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            var claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.GroupSid, courseSid.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                //ExpiresUtc = DateTime.UtcNow.(200),
                IsPersistent = true
            }, identity);


            return View(courseSid);

        }

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public ActionResult Content(int courseSid)
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            List<Content> items = new List<Content>();
            using (var contentManager = new ContentManager())
            {
                message = string.Empty;
                var contents = contentManager.GetContentsByCourseSid(courseSid, out message);
                if (contents != null)
                {
                    items = contents.ToList();
                }
            }
            ViewBag.CourseSid = courseSid;
            return View(items);
        }
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public FileResult Download(int courseSid, string GUIDFileName, string originalFileName)
        {
            if (GetLoginUser() == null)
            {
                return null;
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return null;
            }
            message = string.Empty;
            string path;
            using (var contentManager = new ContentManager())
            {
                path = contentManager.GetContentPathByContentGUIDName(GUIDFileName, out message);
            }
            var file = File(path, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
            if (file == null)
                return null;
            return file;
        }

        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public ActionResult CourseList()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                var courseList = courseManager.GetEnrolledCoursesByStudentSid(GetLoginUser().Students.FirstOrDefault().Sid, out message);
                return View(courseList);
            }
        }
    }
}