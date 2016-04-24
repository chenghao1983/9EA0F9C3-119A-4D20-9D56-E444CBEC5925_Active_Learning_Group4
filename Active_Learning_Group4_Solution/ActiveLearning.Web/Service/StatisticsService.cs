using ActiveLearning.Repository.Context;
//using ActiveLearning.Web.Hubs;
using ActiveLearning.Web.ViewModel;
using Microsoft.AspNet.SignalR;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ActiveLearning.Business.ViewModel;

namespace ActiveLearning.Web.Service
{
    public class StatisticsService
    {
        ActiveLearningContext db = new ActiveLearningContext();

        public async Task NotifyUpdates()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StatisticsHub>();
            if (hubContext != null)
            {
                var stats = await this.GenerateStatistics();
                hubContext.Clients.All.updateStatistics(stats);
            }
        }

        public async Task<StatisticsViewModel> GenerateStatistics()
        {
            var correctAnswers = await db.QuizAnswers.CountAsync(a => a.QuizOption.IsCorrect);
            var totalAnswers = await db.QuizAnswers.CountAsync();
            var totalUsers = (float)await db.QuizAnswers.GroupBy(a => a.StudentSid).CountAsync();

            var incorrectAnswers = totalAnswers - correctAnswers;

            return new StatisticsViewModel
            {
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers,
                TotalAnswers = totalAnswers,
                CorrectAnswersAverage = (totalUsers > 0) ? correctAnswers / totalUsers : 0,
                IncorrectAnswersAverage = (totalUsers > 0) ? incorrectAnswers / totalUsers : 0,
                TotalAnswersAverage = (totalUsers > 0) ? totalAnswers / totalUsers : 0,
            };
        }

    }
}