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
using ActiveLearning.Business.Common;
using System.Transactions;

namespace ActiveLearning.Business.Implementation
{
    public class CourseManager : BaseManager, ICourseManager
    {
        #region Course
        public bool CourseNameExists(string courseName, out string message)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                message = Constants.Empty + courseName_str;
                return true;
            }
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var Course = unitOfWork.Courses.Find(c => c.CourseName.Equals(courseName, StringComparison.CurrentCultureIgnoreCase) && !c.DeleteDT.HasValue);
                if (Course != null)
                {
                    message = courseName + Constants.Already_Exists;
                    return true;
                }
            }
            message = string.Empty;
            return false;
        }
        public Course GetCourseByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var Course = unitOfWork.Courses.Find(c => c.Sid == courseSid && !c.DeleteDT.HasValue).FirstOrDefault();

                    if (Course == null)
                    {
                        message = course_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return Course;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + course_str + Constants.Contact_System_Admin;
                return null;
            }
        }

        public IEnumerable<Course> GetAllCourses(out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var Course = unitOfWork.Courses.Find(c => !c.DeleteDT.HasValue);

                    if (Course == null || Course.Count() == 0)
                    {
                        message = course_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return Course;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + course_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Course AddCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.Empty + course_str;
                return null;
            }
            if (CourseNameExists(course.CourseName, out message))
            {
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        course.CreateDT = DateTime.Now;
                        unitOfWork.Courses.Add(course);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return course;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "adding " + course_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public bool UpdateCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.Empty + course_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        course.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(course, unitOfWork.Courses.Get(course.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "updating " + course_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.Empty + course_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        course.DeleteDT = DateTime.Now;
                        Util.CopyNonNullProperty(course, unitOfWork.Users.Get(course.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "deleting " + course_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        #endregion

        #region Student Enrolment
        public IEnumerable<Student> GetEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            List<Student> list = new List<Student>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var students_Course_Map = unitOfWork.Student_Course_Maps.Find(m => m.CourseSid == courseSid);
                    if (students_Course_Map == null || students_Course_Map.Count() == 0)
                    {
                        message = Constants.No + EnrolledStudent_str;
                        return null;
                    }
                    foreach (var map in students_Course_Map)
                    {
                        using (var userManager = new UserManager())
                        {
                            var student = userManager.GetActiveStudentByStudentSid(map.StudentSid, out message);
                            {
                                if (student != null)
                                {
                                    list.Add(student);
                                }
                            }
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.No + EnrolledStudent_str;
                        return null;
                    }
                    message = string.Empty;
                    return list;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + EnrolledStudent_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<int> GetEnrolledStudentSidsByCourseSid(int courseSid, out string message)
        {
            var enrolledStudents = GetEnrolledStudentsByCourseSid(courseSid, out message);
            if (enrolledStudents == null || enrolledStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledStudents.Select(s => s.Sid);
        }
        public IEnumerable<int> GetEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            var enrolledStudents = GetEnrolledStudentsByCourseSid(courseSid, out message);
            if (enrolledStudents == null || enrolledStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledStudents.Select(s => s.User.Sid);

        }
        public IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (var userManager = new UserManager())
                    {
                        var allActiveStudents = userManager.GetAllActiveStudent(out message);

                        if (allActiveStudents == null || allActiveStudents.Count() == 0)
                        {
                            message = Constants.No + student_str;
                            return null;
                        }

                        var enrolledStudents = GetEnrolledStudentsByCourseSid(courseSid, out message);
                        if (enrolledStudents == null || enrolledStudents.Count() == 0)
                        {
                            message = string.Empty;
                            return allActiveStudents;
                        }
                        if (enrolledStudents.Count() == allActiveStudents.Count())
                        {
                            message = string.Empty;
                            return null;
                        }
                        else
                        {
                            message = string.Empty;
                            return allActiveStudents.SkipWhile(s => enrolledStudents.Select(e => e.Sid).Contains(s.Sid));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + student_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<int> GetNonEnrolledStudentSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();

        }
        public IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool EnrolStudentsToCourse(IEnumerable<Student> students, int courSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool EnrolStudentsToCourse(IEnumerable<int> studentSids, int courSid, out string message) { throw new NotImplementedException(); }
        public bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool RemoveStudentsFromCourse(IEnumerable<int> studentSids, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateStudentsCourseEnrolment(IEnumerable<int> studentSids, int courseSid, out string message) { throw new NotImplementedException(); }
        #endregion

        #region Instructor Enrolment
        public IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetEnrolledInstructorSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<Instructor> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetNonEnrolledInstructorSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetNonEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int InstructorSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int InstructorSid, out string message) { throw new NotImplementedException(); }
        public IEnumerable<int> GetNonEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message) { throw new NotImplementedException(); }
        public bool EnrolInstructorsToCourse(IEnumerable<Instructor> Instructors, int courSid, out string message) { throw new NotImplementedException(); }
        public bool EnrolInstructorsToCourse(IEnumerable<int> InstructorSids, int courSid, out string message) { throw new NotImplementedException(); }
        public bool RemoveInstructorsFromCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool RemoveInstructorsFromCourse(IEnumerable<int> InstructorSids, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<Instructor> Instructors, int courseSid, out string message) { throw new NotImplementedException(); }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<int> InstructorSids, int courseSid, out string message) { throw new NotImplementedException(); }
        #endregion
    }
}
