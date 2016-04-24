//using ActiveLearning.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.ViewModel;

namespace ActiveLearning.Web.Controllers
{
    public class QuizStatisticsController : BaseController
    {
        // GET: QuizStatistics
        public async Task<ActionResult> Index()
        {
            //var statisticsService = new StatisticsService();
            using (var quizManager = new QuizManager())
            {
                // TODO: change to parameter
                int courseSid = 1;
                return View(await quizManager.GenerateStatistics(1));
            }
        }
    }
}