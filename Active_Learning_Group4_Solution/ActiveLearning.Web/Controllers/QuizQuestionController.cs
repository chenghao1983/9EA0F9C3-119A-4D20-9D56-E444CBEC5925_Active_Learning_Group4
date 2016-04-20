using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using ActiveLearning.Repository.Context;
using ActiveLearning.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ActiveLearning.Web.Controllers
{
    public class QuizQuestionController : ApiController
    {
        private ActiveLearningContext db;

        private QuizManager quizManager = new QuizManager();
        private StatisticsService statisticsService;

        public QuizQuestionController()
        {
            this.db = new ActiveLearningContext();
            this.statisticsService = new StatisticsService();
        }






    }
}
