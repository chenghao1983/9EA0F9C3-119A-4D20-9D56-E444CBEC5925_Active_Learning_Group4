using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Entities.ViewModel;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            User user = new DB.User();
            user.Username = "superman";
            user.Password = "police";

            IUserManager userMaanager = new UserManager();
            //userMaanager.AddNewInstructor(user);
            userMaanager.isAuthenticated(user);

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}