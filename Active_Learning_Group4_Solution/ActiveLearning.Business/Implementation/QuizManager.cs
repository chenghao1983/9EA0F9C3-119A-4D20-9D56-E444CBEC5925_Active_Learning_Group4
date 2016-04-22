using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;
using System.Data.Entity;


namespace ActiveLearning.Business.Implementation
{
    public class QuizManager : BaseManager, IQuizManager
    {
        ActiveLearningContext db = new ActiveLearningContext();
       public bool QuizQuestionTitleExists(string quizTitle, out string message) { throw new NotImplementedException(); }
        public QuizQuestion GetQuizQuestionByQuizQuestionSid(int quizQuestionSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<QuizQuestion> GetActiveQuizQuestionsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
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
        public async Task<QuizQuestion> NextQuestionAsync(int userId, int CourseSid)
        {


            var lastQuestionId = await db.QuizAnswers
                .Where(a => a.StudentSid == userId  )
                .GroupBy(a => a.QuizQuestionSid)
                .Select(g => new { QuestionId = g.Key, Count = g.Count() })
                .OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId })
                .Select(q => q.QuestionId)
                .FirstOrDefaultAsync();

            var questionsCount = await db.QuizQuestions.Where(x=> x.CourseSid == CourseSid).CountAsync();

            var nextQuestionId = (lastQuestionId % questionsCount) + 1;

            return await db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);

        }
        public async Task<bool> StoreAsync(QuizAnswer answer)
        {
            this.db.QuizAnswers.Add(answer);

            await this.db.SaveChangesAsync();

            var selectedOption = await this.db.QuizOptions.FirstOrDefaultAsync(o => o.Sid == answer.QuizOptionSid
                && o.QuizQuestionSid == answer.QuizQuestionSid);

            return selectedOption.IsCorrect;
        }

        public Task<QuizQuestion> NextQuestionAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
