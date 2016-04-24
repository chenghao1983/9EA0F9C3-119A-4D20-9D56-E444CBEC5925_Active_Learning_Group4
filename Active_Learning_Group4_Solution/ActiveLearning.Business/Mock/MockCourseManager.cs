﻿using ActiveLearning.Business.Interface;
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
using ActiveLearning.Business.Implementation;

namespace ActiveLearning.Business.Mock
{
    public class MockCourseManager : ICourseManager, IManagerFactoryBase<ICourseManager>
    {
        public ICourseManager Create()
        {
            return new MockCourseManager();
        }

        public void Dispose()
        {
            
        }

        #region Course
        public bool CourseNameExists(string courseName, out string message)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                message = Constants.ValueIsEmpty(Constants.CourseName);
                return true;
            }
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var Course = unitOfWork.Courses.Find(c => c.CourseName.Equals(courseName, StringComparison.CurrentCultureIgnoreCase) && !c.DeleteDT.HasValue).FirstOrDefault();
                if (Course != null)
                {
                    message = Constants.ValueAlreadyExists(courseName);
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
                        message = Constants.ValueNotFound(Constants.Course);
                        return null;
                    }
                    message = string.Empty;
                    return Course;
                }
            }
            catch (Exception ex)
            {

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
                        message = Constants.ValueNotFound(Constants.Course);
                        return null;
                    }
                    message = string.Empty;
                    return Course.ToList();
                }
            }
            catch (Exception ex)
            {
              
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Course);
                return null;
            }
        }
        public Course AddCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
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
             
                message = Constants.OperationFailedDuringAddingValue(Constants.Course);
                return null;
            }
        }
        public bool UpdateCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
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
               
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Course);
                return false;
            }
        }
        public bool DeleteCourse(Course course, out string message)
        {
            if (course == null || course.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return false;
            }
            return DeleteCourse(course.Sid, out message);
        }
        public bool DeleteCourse(int courseSid, out string message)
        {
            message = string.Empty;
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return false;
            }
            var course = GetCourseByCourseSid(courseSid, out message);
            if (course == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Courses.Get(courseSid).DeleteDT = DateTime.Now;
                        unitOfWork.Student_Course_Maps.RemoveRange(unitOfWork.Student_Course_Maps.Find(m => m.CourseSid == course.Sid));
                        unitOfWork.Instructor_Course_Maps.RemoveRange(unitOfWork.Instructor_Course_Maps.Find(m => m.CourseSid == course.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
             
                message = Constants.OperationFailedDuringDeletingValue(Constants.Course);
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
                    var student_Course_Map = unitOfWork.Student_Course_Maps.Find(m => m.CourseSid == courseSid);
                    if (student_Course_Map == null || student_Course_Map.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledStudent);
                        return null;
                    }
                    foreach (var map in student_Course_Map)
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
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledStudent);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
              
                message = Constants.OperationFailedDuringRetrievingValue(Constants.EnrolledStudent);
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
            return enrolledStudents.Select(s => s.Sid).ToList();
        }
        public IEnumerable<int> GetEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            var enrolledStudents = GetEnrolledStudentsByCourseSid(courseSid, out message);
            if (enrolledStudents == null || enrolledStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledStudents.Select(s => s.User.Sid).ToList();

        }
        public IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            IEnumerable<Student> list = new List<Student>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (var userManager = new UserManager())
                    {
                        var allActiveStudents = userManager.GetAllActiveStudent(out message);

                        if (allActiveStudents == null || allActiveStudents.Count() == 0)
                        {
                            message = Constants.ThereIsNoValueFound(Constants.Student);
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
                            message = Constants.ThereIsNoValueFound(Constants.NonEnrolledStudent);
                            return null;
                        }
                        else
                        {
                            list = allActiveStudents.SkipWhile(a => enrolledStudents.Select(e => e.Sid).Contains(a.Sid));
                            if (list == null || list.Count() == 0)
                            {
                                message = Constants.ThereIsNoValueFound(Constants.NonEnrolledStudent);
                                return null;
                            }
                            else
                            {
                                message = string.Empty;
                                return list.ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
                message = Constants.OperationFailedDuringRetrievingValue(Constants.NonEnrolledStudent);
                return null;
            }
        }
        public IEnumerable<int> GetNonEnrolledStudentSidsByCourseSid(int courseSid, out string message)
        {
            var nonEnrolledStudents = GetNonEnrolledStudentsByCourseSid(courseSid, out message);
            if (nonEnrolledStudents == null || nonEnrolledStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledStudents.Select(s => s.Sid).ToList();
        }
        public IEnumerable<int> GetNonEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            var nonEnrolledStudents = GetNonEnrolledStudentsByCourseSid(courseSid, out message);
            if (nonEnrolledStudents == null || nonEnrolledStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledStudents.Select(s => s.User.Sid).ToList();

        }
        public IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            List<Course> list = new List<Course>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var students_Course_Map = unitOfWork.Student_Course_Maps.Find(m => m.StudentSid == studentSid);
                    if (students_Course_Map == null || students_Course_Map.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledCourse);
                        return null;
                    }
                    foreach (var map in students_Course_Map)
                    {
                        var course = GetCourseByCourseSid(map.CourseSid, out message);
                        {
                            if (course != null)
                            {
                                list.Add(course);
                            }
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledCourse);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
           
                message = Constants.OperationFailedDuringRetrievingValue(Constants.EnrolledCourse);
                return null;
            }
        }
        public IEnumerable<int> GetEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            var enrolledCourses = GetEnrolledCoursesByStudentSid(studentSid, out message);
            if (enrolledCourses == null || enrolledCourses.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledCourses.Select(c => c.Sid).ToList();
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            IEnumerable<Course> list = new List<Course>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var allCourses = GetAllCourses(out message);
                    if (allCourses == null || allCourses.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Course);
                        return null;
                    }

                    var enrolledCourses = GetEnrolledCoursesByStudentSid(studentSid, out message);
                    if (enrolledCourses == null || enrolledCourses.Count() == 0)
                    {
                        message = string.Empty;
                        return allCourses.ToList();
                    }
                    if (enrolledCourses.Count() == allCourses.Count())
                    {
                        message = Constants.ThereIsNoValueFound(Constants.NonEnrolledCourse);
                        return null;
                    }
                    else
                    {
                        list = allCourses.SkipWhile(a => enrolledCourses.Select(e => e.Sid).Contains(a.Sid));
                        if (list == null || list.Count() == 0)
                        {
                            message = Constants.ThereIsNoValueFound(Constants.NonEnrolledCourse);
                            return null;
                        }
                        else
                        {
                            message = string.Empty;
                            return list.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
                message = Constants.OperationFailedDuringRetrievingValue(Constants.NonEnrolledCourse);
                return null;
            }
        }
        public IEnumerable<int> GetNonEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            var nonEnrolledCourse = GetNonEnrolledCoursesByStudentSid(studentSid, out message);
            if (nonEnrolledCourse == null || nonEnrolledCourse.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledCourse.Select(e => e.Sid).ToList();
        }
        public bool EnrolStudentsToCourse(IEnumerable<Student> students, int courseSid, out string message)
        {
            return UpdateStudentsCourseEnrolment(students, courseSid, out message);
        }

        public bool EnrolStudentsToCourse(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            return UpdateStudentsCourseEnrolment(studentSids, courseSid, out message);
        }

        public bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message)
        {
            return UpdateStudentsCourseEnrolment(students, courseSid, out message);
        }

        public bool RemoveStudentsFromCourse(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            return UpdateStudentsCourseEnrolment(studentSids, courseSid, out message);
        }

        public bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message)
        {
            if (students == null || students.Count() == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return false;
            }
            return UpdateStudentsCourseEnrolment(students.Select(s => s.Sid), courseSid, out message);
        }
        public bool UpdateStudentsCourseEnrolment(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            if (studentSids == null || studentSids.Count() == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }

            IEnumerable<int> studentSidsToEnrol = new List<int>();
            IEnumerable<int> studentSidsToRemove = new List<int>();

            var currentStudentSids = GetEnrolledStudentSidsByCourseSid(courseSid, out message);

            // no student enrolled in the course
            if (currentStudentSids == null || currentStudentSids.Count() == 0)
            {
                studentSidsToEnrol = studentSids;
            }
            try
            {
                studentSidsToEnrol = studentSids.SkipWhile(s => currentStudentSids.Contains(s));
                studentSidsToRemove = currentStudentSids.SkipWhile(s => studentSids.Contains(s));
            }
            catch (Exception ex)
            {
               
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Student_Course_Enrolment);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (studentSidsToEnrol != null)
                        {
                            foreach (int sid in studentSidsToEnrol)
                            {
                                unitOfWork.Student_Course_Maps.Add(new Student_Course_Map() { StudentSid = sid, CourseSid = courseSid, CreateDT = DateTime.Now });
                            }
                        }
                        if (studentSidsToRemove != null)
                        {
                            unitOfWork.Student_Course_Maps.RemoveRange(unitOfWork.Student_Course_Maps.Find(m => studentSidsToRemove.Contains(m.StudentSid) && m.CourseSid == courseSid));
                        }
                        unitOfWork.Complete();
                        scope.Complete();

                        message = string.Empty;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
             
                message = Constants.OperationFailedDuringSavingValue(Constants.Student_Course_Enrolment);
                return false;
            }
        }
        #endregion

        #region Instructor enrolment
        public IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            List<Instructor> list = new List<Instructor>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructor_Course_Map = unitOfWork.Instructor_Course_Maps.Find(m => m.CourseSid == courseSid);
                    if (instructor_Course_Map == null || instructor_Course_Map.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledInstructor);
                        return null;
                    }
                    foreach (var map in instructor_Course_Map)
                    {
                        using (var userManager = new UserManager())
                        {
                            var Instructor = userManager.GetActiveInstructorByInstructorSid(map.InstructorSid, out message);
                            {
                                if (Instructor != null)
                                {
                                    list.Add(Instructor);
                                }
                            }
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledInstructor);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
             
                message = Constants.OperationFailedDuringRetrievingValue(Constants.EnrolledInstructor);
                return null;
            }
        }
        public IEnumerable<int> GetEnrolledInstructorSidsByCourseSid(int courseSid, out string message)
        {
            var enrolledInstructors = GetEnrolledInstructorsByCourseSid(courseSid, out message);
            if (enrolledInstructors == null || enrolledInstructors.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledInstructors.Select(i => i.Sid).ToList();
        }
        public IEnumerable<int> GetEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message)
        {
            var enrolledInstructors = GetEnrolledInstructorsByCourseSid(courseSid, out message);
            if (enrolledInstructors == null || enrolledInstructors.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledInstructors.Select(i => i.User.Sid).ToList();
        }
        public IEnumerable<Instructor> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            IEnumerable<Instructor> list = new List<Instructor>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (var userManager = new UserManager())
                    {
                        var allActiveInstructors = userManager.GetAllActiveInstructor(out message);

                        if (allActiveInstructors == null || allActiveInstructors.Count() == 0)
                        {
                            message = Constants.ThereIsNoValueFound(Constants.Instructor);
                            return null;
                        }

                        var enrolledInstructors = GetEnrolledInstructorsByCourseSid(courseSid, out message);
                        if (enrolledInstructors == null || enrolledInstructors.Count() == 0)
                        {
                            message = string.Empty;
                            return allActiveInstructors.ToList();
                        }
                        if (enrolledInstructors.Count() == allActiveInstructors.Count())
                        {
                            message = Constants.ThereIsNoValueFound(Constants.NonEnrolledInstructor);
                            return null;
                        }
                        else
                        {
                            list = allActiveInstructors.SkipWhile(a => enrolledInstructors.Select(e => e.Sid).Contains(a.Sid));
                            if (list == null || list.Count() == 0)
                            {
                                message = Constants.ThereIsNoValueFound(Constants.NonEnrolledInstructor);
                                return null;
                            }
                            else
                            {
                                message = string.Empty;
                                return list.ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                message = Constants.OperationFailedDuringRetrievingValue(Constants.NonEnrolledInstructor);
                return null;
            }
        }
        public IEnumerable<int> GetNonEnrolledInstructorSidsByCourseSid(int courseSid, out string message)
        {
            var nonEnrolledInstructors = GetNonEnrolledInstructorsByCourseSid(courseSid, out message);
            if (nonEnrolledInstructors == null || nonEnrolledInstructors.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledInstructors.Select(i => i.Sid).ToList();
        }
        public IEnumerable<int> GetNonEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message)
        {
            var nonEnrolledInstructors = GetNonEnrolledInstructorsByCourseSid(courseSid, out message);
            if (nonEnrolledInstructors == null || nonEnrolledInstructors.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledInstructors.Select(i => i.User.Sid).ToList();
        }
        public IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int InstructorSid, out string message)
        {
            message = string.Empty;
            List<Course> list = new List<Course>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var Instructors_Course_Map = unitOfWork.Instructor_Course_Maps.Find(m => m.InstructorSid == InstructorSid);
                    if (Instructors_Course_Map == null || Instructors_Course_Map.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledCourse);
                        return null;
                    }
                    foreach (var map in Instructors_Course_Map)
                    {
                        var course = GetCourseByCourseSid(map.CourseSid, out message);
                        {
                            if (course != null)
                            {
                                list.Add(course);
                            }
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.EnrolledCourse);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
               
                message = Constants.OperationFailedDuringRetrievingValue(Constants.EnrolledCourse);
                return null;
            }
        }
        public IEnumerable<int> GetEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message)
        {
            var enrolledCourses = GetEnrolledCoursesByInstructorSid(InstructorSid, out message);
            if (enrolledCourses == null || enrolledCourses.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return enrolledCourses.Select(c => c.Sid).ToList();
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int InstructorSid, out string message)
        {
            message = string.Empty;
            IEnumerable<Course> list = new List<Course>();
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var allCourses = GetAllCourses(out message);
                    if (allCourses == null || allCourses.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Course);
                        return null;
                    }

                    var enrolledCourses = GetEnrolledCoursesByInstructorSid(InstructorSid, out message);
                    if (enrolledCourses == null || enrolledCourses.Count() == 0)
                    {
                        message = string.Empty;
                        return allCourses.ToList();
                    }
                    if (enrolledCourses.Count() == allCourses.Count())
                    {
                        message = Constants.ThereIsNoValueFound(Constants.NonEnrolledCourse);
                        return null;
                    }
                    else
                    {
                        list = allCourses.SkipWhile(a => enrolledCourses.Select(e => e.Sid).Contains(a.Sid));
                        if (list == null || list.Count() == 0)
                        {
                            message = Constants.ThereIsNoValueFound(Constants.NonEnrolledCourse);
                            return null;
                        }
                        else
                        {
                            message = string.Empty;
                            return list.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
                message = Constants.OperationFailedDuringRetrievingValue(Constants.NonEnrolledCourse);
                return null;
            }
        }
        public IEnumerable<int> GetNonEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message)
        {
            var nonEnrolledCourse = GetNonEnrolledCoursesByInstructorSid(InstructorSid, out message);
            if (nonEnrolledCourse == null || nonEnrolledCourse.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return nonEnrolledCourse.Select(e => e.Sid).ToList();
        }
        public bool EnrolInstructorsToCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            return UpdateInstructorsCourseEnrolment(Instructors, courseSid, out message);
        }
        public bool EnrolInstructorsToCourse(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            return UpdateInstructorsCourseEnrolment(InstructorSids, courseSid, out message);
        }
        public bool RemoveInstructorsFromCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            return UpdateInstructorsCourseEnrolment(Instructors, courseSid, out message);
        }
        public bool RemoveInstructorsFromCourse(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            return UpdateInstructorsCourseEnrolment(InstructorSids, courseSid, out message);
        }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            if (Instructors == null || Instructors.Count() == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            return UpdateInstructorsCourseEnrolment(Instructors.Select(i => i.Sid), courseSid, out message);
        }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            if (InstructorSids == null || InstructorSids.Count() == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }

            IEnumerable<int> InstructorSidsToEnrol = new List<int>();
            IEnumerable<int> InstructorSidsToRemove = new List<int>();

            var currentInstructorSids = GetEnrolledInstructorSidsByCourseSid(courseSid, out message);

            // no Instructor enrolled in the course
            if (currentInstructorSids == null || currentInstructorSids.Count() == 0)
            {
                InstructorSidsToEnrol = InstructorSids;
            }
            try
            {
                InstructorSidsToEnrol = InstructorSids.SkipWhile(s => currentInstructorSids.Contains(s));
                InstructorSidsToRemove = currentInstructorSids.SkipWhile(s => InstructorSids.Contains(s));
            }
            catch (Exception ex)
            {
               
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Instructor_Course_Enrolment);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (InstructorSidsToEnrol != null)
                        {
                            foreach (int sid in InstructorSidsToEnrol)
                            {
                                unitOfWork.Instructor_Course_Maps.Add(new Instructor_Course_Map() { InstructorSid = sid, CourseSid = courseSid, CreateDT = DateTime.Now });
                            }
                        }
                        if (InstructorSidsToRemove != null)
                        {
                            unitOfWork.Instructor_Course_Maps.RemoveRange(unitOfWork.Instructor_Course_Maps.Find(m => InstructorSidsToRemove.Contains(m.InstructorSid) && m.CourseSid == courseSid));
                        }
                        unitOfWork.Complete();
                        scope.Complete();

                        message = string.Empty;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
               
                message = Constants.OperationFailedDuringSavingValue(Constants.Instructor_Course_Enrolment);
                return false;
            }
        }



        #endregion

    }
}
