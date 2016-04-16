﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Interface.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admins { get; }
        IChatDetailRepository ChatDetails { get; }
        IChatRecord_Course_MapRepository ChatRecord_Course_Maps { get; }
        IChatRecordRepository ChatRecords { get; }
        IContentRepository Contents { get; }
        ICourseRepository Courses { get; }
        IInstructor_Course_MapRepository Instructor_Course_Maps { get; }
        IInstructorRepository Instructors { get; }
        IQuiz_Course_MapRepository Quiz_Course_Maps { get; }
        IQuizAnswerRepository QuizAnswers { get; }
        IQuizDetailRepository QuizDetails { get; }
        IQuizRecordRepository QuizRecors { get; }
        IStudent_Course_MapRepository Student_Course_Maps { get; }
        IStudentRepository Students { get; }
        IUserRepository Users { get; }

        int Complete();
    }
}