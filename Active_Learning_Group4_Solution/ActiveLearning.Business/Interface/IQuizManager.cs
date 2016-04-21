using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;

namespace ActiveLearning.Business.Interface
{
  public  interface IQuizManager 
    {
        QuizQuestion GetQuizByQuizSid(int quizSid, out string message);
        IEnumerable<QuizQuestion> GetQuizsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetQuizSidsByCourseSid(int courseSid, out string message);
        IEnumerable<QuizQuestion> GetQuizsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message);
        IEnumerable<int> GetQuizSidsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message);

        Task<QuizQuestion> NextQuestionAsync(int userId);
        Task<bool> StoreAsync(QuizAnswer answer);
    }
}
