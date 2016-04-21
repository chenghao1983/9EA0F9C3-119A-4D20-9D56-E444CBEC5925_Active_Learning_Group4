using ActiveLearning.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Web.Controllers
{
    public class QuizStatisticsController : Controller
    {
        // GET: QuizStatistics
        public async Task<ActionResult> Index()
        {
            var statisticsService = new StatisticsService();

            return View(await statisticsService.GenerateStatistics());
        }
    }
}