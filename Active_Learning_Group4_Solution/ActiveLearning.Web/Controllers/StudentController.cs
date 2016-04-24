using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ActiveLearning.Business.Interface;
using ActiveLearning.Business.Implementation;

namespace ActiveLearning.Web.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string courseSid)
        {
            var claims = new List<Claim>();

            // TODO: groupid : courseSid parameter, name 


            claims.Add(new Claim(ClaimTypes.GroupSid, courseSid));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(200),
                IsPersistent = true
            }, identity);


            return View();

        }

        public ActionResult Download()
        {
            // TODO 
            // Get course ID
            IFileManager fileManager = new FileManager();
            var files = fileManager.GetFiles(2);

            List<string> items = new List<string>();
            foreach (var file in files)
            {
                items.Add(file.OriginalFileName);
            }

            return View(items);
        }

        public FileResult DownloadFile(string FileName)
        {
            // convert filename to GUID filename
            IFileManager fileManager = new FileManager();
            var guidFileName = fileManager.GetGUIDFile(FileName, 2);


            return File("~/App_Data/Upload/" + guidFileName, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
        }
    }
}