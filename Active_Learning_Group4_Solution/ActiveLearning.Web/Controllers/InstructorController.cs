using ActiveLearning.DB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Common;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;

namespace ActiveLearning.Web.Controllers
{
    public class InstructorController : BaseController
    {
        // GET: Instructor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string courseSid)
        {
            var claims = new List<Claim>();

            // TODO: groupid : courseSid parameter, name 
            courseSid = "1";

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
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    var fileName = Path.GetFileName(file.FileName);
                    Content fileDetail = new Content()
                    {
                        //TODO set the courseSID
                        CourseSid = 2,
                        Path = "~/App_Data/Upload/",
                        OriginalFileName = fileName,
                        FileName = guid + Path.GetExtension(fileName),
                        Type = "",
                        CreateDT = DateTime.Now
                    };

                    // Write to Database
                    using (var fileManager = new ContentManager())
                    {
                        string message = string.Empty;
                        fileManager.AddContent(fileDetail, 1, out message);
                    }

                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), guid + Path.GetExtension(fileName));
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upload successful";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed. " + ex.Message;
                return View();
            }
        }
    }
}