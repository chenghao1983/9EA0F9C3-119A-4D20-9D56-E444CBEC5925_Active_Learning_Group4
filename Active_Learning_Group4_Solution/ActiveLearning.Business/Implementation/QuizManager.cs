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

using System.Data.Entity;


namespace ActiveLearning.Business.Implementation
{
    public class QuizManager : BaseManager, IQuizManager
    {
        ActiveLearningContext db = new ActiveLearningContext();

        public QuizQuestion GetQuizByQuizSid(int quizSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuizQuestion> GetQuizsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuizQuestion> GetQuizsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetQuizSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetQuizSidsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

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


















    }
}
