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

namespace ActiveLearning.Business.Implementation
{
    public class CourseManager : ICourseManager
    {
        public void AddCourse(Course course)
        {
            using (var unitOfWork = new UnitOfWork(new ENET_Project_Active_Learning_Group4Entities()))
            {
                unitOfWork.Courses.AddCourse(course);

                unitOfWork.Complete();
            }
        }

        public void EnrolInstructorToCourse(int courseID, int instructorID)
        {
            // check if course exists
            ICourseRepository courseRepo = new CourseRepository();
            if (!courseRepo.IsCourseExist(courseID))
            {
                // course not exists
                throw new CourseNotFondException(courseID + " is not a registered  course.");
            }

            courseRepo.EnrolInstructorToCourse(courseID, instructorID);
        }

        public void EnrolStudentToCourse(int courseID, int studentID)
        {
            // check if course exists
            ICourseRepository courseRepo = new CourseRepository();
            if (!courseRepo.IsCourseExist(courseID))
            {
                // course not exists
                throw new CourseNotFondException(courseID + " is not a registered  course.");
            }

            courseRepo.EnrolStudentToCourse(courseID, studentID);

        }

        public List<Course> GetCoursesByInstructor(int instructorID)
        {
            ICourseRepository courseRepo = new CourseRepository();


            return courseRepo.GetCoursesByInstructor(instructorID);
        }

        public List<Course> GetCoursesByStudent(int studentID)
        {

            ICourseRepository courseRepo = new CourseRepository();


            return courseRepo.GetCoursesByStudent(studentID);

        }

        public void RemoveInstructorFromCourse(int courseID, int instructorID)
        {
            ICourseRepository courseRepo = new CourseRepository();


             courseRepo.RemoveInstructorFromCourse(courseID, instructorID);
        }

        public void RemoveStudentFromCourse(int courseID, int studentID)
        {
            ICourseRepository courseRepo = new CourseRepository();


            courseRepo.RemoveStudentFromCourse(courseID, studentID);
        }
    }
}
