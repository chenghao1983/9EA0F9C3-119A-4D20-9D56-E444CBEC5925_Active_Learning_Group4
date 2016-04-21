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

        public async Task<QuizQuestion> Get()
        {
            //var userId = User.Identity.Name;

            int userId = 1;

            QuizQuestion nextQuestion = await this.quizManager.NextQuestionAsync(userId);

            if (nextQuestion == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return nextQuestion;
        }

        public async Task<HttpResponseMessage> Post(QuizAnswer answer)
        {
            if (ModelState.IsValid)
            {
                //answer.StudentSid = User.Identity.Name;

                //TODO
                answer.StudentSid = 1;

                answer.CreateDT = DateTime.Now;
                var isCorrect = await this.quizManager.StoreAsync(answer);

                await this.statisticsService.NotifyUpdates();

                return Request.CreateResponse(HttpStatusCode.Created, isCorrect);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }





    }
}
