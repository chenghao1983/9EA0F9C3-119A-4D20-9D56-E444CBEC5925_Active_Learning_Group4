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
        #region Course
      public  Course GetCourseByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Course> GetAllCourses(out string message)
        {
            message = string.Empty;
            return null;
        }
        public void AddCourse(Course course, out string message)
        {
            message = string.Empty;
        }
        public bool UpdateCourse(Course course, out string message)
        {
            message = string.Empty;
            return false;
        }
        public bool DeleteCourse(Course course, out string message)
        {
            message = string.Empty;
            return false;
        }
        #endregion

        #region Student Enrolment
        public IEnumerable<Student> GetEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public  IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public bool EnrolStudentsToCourse(IEnumerable<Student> students, int courSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        public bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        public bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        #endregion

        #region Instructor Enrolment
        public IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Student> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int instructorSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int instructorSid, out string message)
        {
            message = string.Empty;
            return null;
        }
        public bool EnrolInstructorsToCourse(IEnumerable<Instructor> instructor, int courseSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        public bool RemoveInstructorsFromCourse(IEnumerable<Instructor> instructor, int courseSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        public bool UpdateInstructorCourseEnrolment(IEnumerable<Instructor> instructors, int courseSid, out string message)
        {
            message = string.Empty;
            return false;
        }
        #endregion
    }
}
