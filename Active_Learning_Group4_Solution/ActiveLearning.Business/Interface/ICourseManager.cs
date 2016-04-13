using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface ICourseManager
    {

        void AddCourse(Course course);

        IEnumerable<Course> GetCoursesByStudent(int studentID);

        IEnumerable<Course> GetCoursesByInstructor(int instructorID);

        void EnrolStudentToCourse(int courseID, int studentID);

        void EnrolInstructorToCourse(int courseID, int instructorID);

        void RemoveStudentFromCourse(int courseID, int studentID);

        void RemoveInstructorFromCourse(int courseID, int instructorID);





    }
}
