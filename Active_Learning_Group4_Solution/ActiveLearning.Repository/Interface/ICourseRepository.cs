using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Interface
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);

        bool IsCourseExist(int courseID);

        void EnrolStudentToCourse(int courseID, int studentID);

        void EnrolInstructorToCourse(int courseID, int instructorID);

        List<Course> GetCoursesByStudent(int studentID);

        List<Course> GetCoursesByInstructor(int instructorID);

        void RemoveStudentFromCourse(int courseID, int studentID);

        void RemoveInstructorFromCourse(int courseID, int instructorID);






    }
}
