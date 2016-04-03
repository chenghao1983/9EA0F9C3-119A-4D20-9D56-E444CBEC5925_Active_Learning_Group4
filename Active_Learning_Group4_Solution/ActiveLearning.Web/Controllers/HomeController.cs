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
            using (var unitOfWork = new UnitOfWork(new ENET_Project_Active_Learning_Group4Entities()))
            {
                // Example1
                unitOfWork.Users.AddUserAccount(new UserViewModel() { LoginName="lame",Password="asd"});

                unitOfWork.Complete();
            }

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