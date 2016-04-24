using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using ActiveLearning.Repository.Context;
//using ActiveLearning.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using ActiveLearning.Business.ViewModel;
using System.Web.SessionState;

namespace ActiveLearning.Web.Controllers
{
    public class QuizQuestionController : ApiController
    {
        //private ActiveLearningContext db;

        //private QuizManager quizManager = new QuizManager();
        //private StatisticsService statisticsService;

        public QuizQuestionController()
        {
            //this.db = new ActiveLearningContext();
            //this.statisticsService = new StatisticsService();
        }

        public async Task<QuizQuestion> Get()
        {
            //var userId = User.Identity.Name;
            using (var quizManager = new QuizManager())
            {
                int userId = 1;
                int courseID = 1;

                var session = SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);

                if (session == null || session[BaseController.UserSessionParam] ==null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                
                User user = session[BaseController.UserSessionParam] as User;


                QuizQuestion nextQuestion = await quizManager.NextQuestionAsync(user.Students.FirstOrDefault().Sid, courseID);

                if (nextQuestion == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return nextQuestion;
            }
        }

        public async Task<HttpResponseMessage> Post(QuizAnswer answer)
        {
            if (ModelState.IsValid)
            {
                //answer.StudentSid = User.Identity.Name;

                //TODO
                int userId = 1;
                int courseID = 1;

                answer.StudentSid = 1;
                answer.CreateDT = DateTime.Now;

                // await this.statisticsService.NotifyUpdates();

                using (var quizManager = new QuizManager())
                {
                    var isCorrect = await quizManager.StoreAsync(answer);

                    //TODO: change parameter
                    await quizManager.NotifyUpdates(courseID);
                    return Request.CreateResponse(HttpStatusCode.Created, isCorrect);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            //this.db.Dispose();
            base.Dispose(disposing);
        }





    }
}
