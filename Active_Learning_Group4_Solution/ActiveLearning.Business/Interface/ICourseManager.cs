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
        #region Course
        Course GetCourseByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetAllCourses(out string message);
        void AddCourse(Course course, out string message);
        bool UpdateCourse(Course course, out string message);
        bool DeleteCourse(Course course, out string message);
        #endregion

        #region Student Enrolment
        IEnumerable<Student> GetEnrolledStudentsByCourseSid(int courseSid, out string message);
        IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message);
        IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message);
        bool EnrolStudentsToCourse(IEnumerable<Student> students, int courSid, out string message);
        bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message);
        bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message);
        #endregion

        #region Instructor Enrolment
        IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message);
        IEnumerable<Student> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int instructorSid, out string message);
        IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int instructorSid, out string message);
        bool EnrolInstructorsToCourse(IEnumerable<Instructor> instructor, int courseSid, out string message);
        bool RemoveInstructorsFromCourse(IEnumerable<Instructor> instructor, int courseSid, out string message);
        bool UpdateInstructorCourseEnrolment(IEnumerable<Instructor> instructors, int courseSid, out string message);
        #endregion
    }
}
