using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
namespace ActiveLearning.Web.Controllers
{
    public class ChatController : BaseController
    {
        // GET: Chat
        public ActionResult Index()
        {
            //TODO
            var claims = new List<Claim>();

            // TODO: groupid : courseSid parameter, name 
            claims.Add(new Claim(ClaimTypes.GroupSid, "CourseName"));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(200),
                IsPersistent = true
            }, identity);


            return View();
        }
    }
}