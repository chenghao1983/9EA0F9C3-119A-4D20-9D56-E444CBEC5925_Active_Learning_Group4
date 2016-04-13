using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.CustomExcepetion;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Context;

namespace ActiveLearning.Business.Implementation
{
    public class CourseManager : BaseManager, ICourseManager
    {
        public void AddCourse(Course course)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                unitOfWork.Courses.Add(course);

                unitOfWork.Complete();
            }
        }

        public void EnrolInstructorToCourse(int courseID, int instructorID)
        {

        }

        public void EnrolStudentToCourse(int courseID, int studentID)
        {

        }

        public IEnumerable<Course> GetCoursesByInstructor(int instructorID)
        {
            // TODO
            return null;
        }

        public IEnumerable<Course> GetCoursesByStudent(int studentID)
        {
            // TODO
            return null;

        }

        public void RemoveInstructorFromCourse(int courseID, int instructorID)
        {

        }

        public void RemoveStudentFromCourse(int courseID, int studentID)
        {

        }
    }
}
