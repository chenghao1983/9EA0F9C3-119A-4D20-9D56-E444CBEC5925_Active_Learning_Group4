using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;
using System.Data.Entity;
using ActiveLearning.Business.ViewModel;
using Microsoft.AspNet.SignalR;
namespace ActiveLearning.Business.Implementation
{
    public class QuizManager : BaseManager, IQuizManager
    {
        ActiveLearningContext db = new ActiveLearningContext();

        #region Normal
        public bool QuizQuestionTitleExists(string quizTitle, out string message)
        {
            if (string.IsNullOrEmpty(quizTitle))
            {
                message = Constants.ValueIsEmpty(Constants.QuizTitle);
                return true;
            }
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var quizQuestion = unitOfWork.QuizQuestions.Find(q => q.Title.Equals(quizTitle, StringComparison.CurrentCultureIgnoreCase) && !q.DeleteDT.HasValue).FirstOrDefault();
                if (quizQuestion != null)
                {
                    message = Constants.ValueAlreadyExists(quizTitle);
                    return true;
                }
            }
            message = string.Empty;
            return false;
        }
        public QuizQuestion GetQuizQuestionByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<QuizQuestion> GetActiveQuizQuestionsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetActiveQuizQuestionSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<QuizQuestion> GetDeletedQuizQuestionsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetDeletedQuizQuestionSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public QuizQuestion AddQuizQuestionToCourse(QuizQuestion quizQuestion, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateQuizQuestion(QuizQuestion quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizQuestion(QuizQuestion quizQuestion, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizQuestion(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public QuizOption GetQuizOptionByQuizOptionSid(int quizOptionSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<QuizOption> GetQuizOptionsByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetQuizOptionSidsByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public QuizOption AddQuizOptionToQuizQuestion(QuizOption quizOption, int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateQuizOption(QuizOption quizOption, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizOption(QuizOption quizOption, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizOption(int quizOptionSid, out string message) { throw new NotImplementedException(); }
        public QuizAnswer GetQuizAnswerByQuizAnswerSid(int quizAnswerSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<QuizAnswer> GetQuizAnswersByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetQuizAnswerSidsByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public QuizAnswer AddQuizAnswerToQuizQuestionAndOption(QuizAnswer quizAnswer, int quizQuestionSid, int quizOptionSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateQuizAnswer(QuizAnswer quizAnswer, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizAnswer(QuizAnswer quizAnswer, out string message) { throw new NotImplementedException(); }
        public bool DeleteQuizAnswer(int quizAnswerSid, out string message) { throw new NotImplementedException(); }
        #endregion

        #region Async
        public async Task<QuizQuestion> NextQuestionAsync(int studentSid, int CourseSid)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                //var lastQuestionId = await unitOfWork.QuizAnswers.FindAsync(a => a.StudentSid == studentSid)
            }
            var lastQuestionId = await db.QuizAnswers
                .Where(a => a.StudentSid == studentSid)
                .GroupBy(a => a.QuizQuestionSid)
                .Select(g => new { QuestionId = g.Key, Count = g.Count() })
                .OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId })
                .Select(q => q.QuestionId)
                .FirstOrDefaultAsync();

            try
            {
                var questionsCount = await db.QuizQuestions.Where(x => x.CourseSid == CourseSid).CountAsync();

                if (questionsCount == 0)
                {
                    return null;
                }
                var nextQuestionId = (lastQuestionId % questionsCount) + 1;

                //var p = db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);


                return await db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return null;

            }
        }
        public async Task<bool> StoreAsync(QuizAnswer answer)
        {
            this.db.QuizAnswers.Add(answer);

            await this.db.SaveChangesAsync();

            var selectedOption = await this.db.QuizOptions.FirstOrDefaultAsync(o => o.Sid == answer.QuizOptionSid
                && o.QuizQuestionSid == answer.QuizQuestionSid);

            return selectedOption.IsCorrect;
        }
        public async Task NotifyUpdates(int courseSid)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StatisticsHub>();
            if (hubContext != null)
            {
                var stats = await this.GenerateStatistics(courseSid);
                hubContext.Clients.All.updateStatistics(stats);
            }
        }
        public async Task<StatisticsViewModel> GenerateStatistics(int courseSid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var correctAnswerCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.IsCorrect && a.QuizOption.QuizQuestion.CourseSid == courseSid)).Count();
                    var totalAnswerCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.QuizQuestion.CourseSid == courseSid)).Count();
                    var totalUserCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.QuizQuestion.CourseSid == courseSid)).Select(s => s.StudentSid).Count();
                    var incorrectAnswers = totalAnswerCount - correctAnswerCount;

                    return new StatisticsViewModel
                    {
                        CorrectAnswers = correctAnswerCount,
                        IncorrectAnswers = incorrectAnswers,
                        TotalAnswers = totalAnswerCount,
                        CorrectAnswersAverage = (totalUserCount > 0) ? correctAnswerCount / totalUserCount : 0,
                        IncorrectAnswersAverage = (totalUserCount > 0) ? incorrectAnswers / totalUserCount : 0,
                        TotalAnswersAverage = (totalUserCount > 0) ? totalAnswerCount / totalUserCount : 0,
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return null;
            }


            //var correctAnswerCount = await db.QuizAnswers.CountAsync(a => a.QuizOption.IsCorrect);
            // var totalAnswerCount = await db.QuizAnswers.CountAsync();
            //var totalUserCount = (float)await db.QuizAnswers.GroupBy(a => a.StudentSid).CountAsync();

            //var incorrectAnswers = totalAnswerCount - correctAnswerCount;

            //return new StatisticsViewModel
            //{
            //    CorrectAnswers = correctAnswerCount,
            //    IncorrectAnswers = incorrectAnswers,
            //    TotalAnswers = totalAnswerCount,
            //    CorrectAnswersAverage = (totalUserCount > 0) ? correctAnswerCount / totalUserCount : 0,
            //    IncorrectAnswersAverage = (totalUserCount > 0) ? incorrectAnswers / totalUserCount : 0,
            //    TotalAnswersAverage = (totalUserCount > 0) ? totalAnswerCount / totalUserCount : 0,
            //};
        }
        #endregion
    }
}
