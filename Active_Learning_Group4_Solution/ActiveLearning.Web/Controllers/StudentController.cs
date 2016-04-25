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
                return new HttpUnauthorizedResult(message);
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
        public ActionResult Download()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            // TODO 
            // Get course ID
            string message = string.Empty;
            IContentManager fileManager = new ContentManager();
            var files = fileManager.GetContentsByCourseSid(1, out message);

            List<string> items = new List<string>();
            foreach (var file in files)
            {
                items.Add(file.OriginalFileName);
            }

            return View(items);
        }
        [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
        public FileResult DownloadFile(string FileName)
        {
            if (GetLoginUser() == null)
            {
                return null;
            }
            // convert filename to GUID filename
            string message = string.Empty;
            IContentManager fileManager = new ContentManager();
            var guidFileName = fileManager.GetGUIDFile(FileName, 2, out message);


            return File("~/App_Data/Upload/" + guidFileName, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
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