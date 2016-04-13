using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Interface.Core;
using ActiveLearning.Repository.Context;
using ActiveLearning.Repository.Repository.Core;
using ActiveLearning.Repository.Repository;
using ActiveLearning.DB;

namespace ActiveLearning.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ActiveLearningContext _context;

        public UnitOfWork(ActiveLearningContext context)
        {
            _context = context;

            Admins = new AdminRepository(_context);
            ChatDetails = new ChatDetailRepository(_context);
            ChatRecord_Course_Maps = new ChatRecord_Course_MapRepository(_context);
            ChatRecords = new ChatRecordRepository(_context);
            Contents = new ContentRepository(_context);
            Courses = new CourseRepository(_context);
            Instructor_Course_Maps = new Instructor_Course_MapRepository(_context);
            Instructors = new InstructorRepository(_context);
            Quiz_Course_Maps = new Quiz_Course_MapRepository(_context);
            QuizAnswers = new QuizAnswerRepository(_context);
            QuizDetails = new QuizDetailRepository(_context);
            QuizRecors = new QuizRecordRepository(_context);
            Student_Course_Maps = new Student_Course_MapRepository(_context);
            Students = new StudentRepository(_context);
            Users = new UserRepository(_context);
        }
        public IAdminRepository Admins { get; private set; }
        public IChatDetailRepository ChatDetails { get; private set; }
        public IChatRecord_Course_MapRepository ChatRecord_Course_Maps { get; private set; }
        public IChatRecordRepository ChatRecords { get; private set; }
        public IContentRepository Contents { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IInstructor_Course_MapRepository Instructor_Course_Maps { get; private set; }
        public IInstructorRepository Instructors { get; private set; }
        public IQuiz_Course_MapRepository Quiz_Course_Maps { get; private set; }
        public IQuizAnswerRepository QuizAnswers { get; private set; }
        public IQuizDetailRepository QuizDetails { get; private set; }
        public IQuizRecordRepository QuizRecors { get; private set; }
        public IStudent_Course_MapRepository Student_Course_Maps { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IUserRepository Users { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
