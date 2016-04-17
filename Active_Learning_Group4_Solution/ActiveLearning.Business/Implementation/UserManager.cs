using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Context;
using System.Transactions;
using ActiveLearning.Business.Common;
using System.Collections;
using System.Collections.Generic;

namespace ActiveLearning.Business.Implementation
{
    public class UserManager : BaseManager, IUserManager
    {
        #region Student
        public Student GetActiveStudentByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var student = unitOfWork.Students.Get(studentSid);
                    if (student != null)
                    {
                        student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();
                    }
                    else
                    {
                        message = "Student" + Constants.Not_Found;
                        return null;
                    }
                    return student;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving student" + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerator<Student> GetAllActiveStudent(out string message)
        {
            message = string.Empty;
            List<Student> list = new List<Student>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var students = unitOfWork.Students.GetAll();
                    if (students != null && students.Count() > 0)
                    {
                        foreach (var student in students)
                        {
                            student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();
                            if (student.User != null)
                            {
                                list.Add(student);
                            }
                        }
                    }
                    else
                    {
                        message = Constants.No + "student";
                        return null;
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.No + "student";
                        return null;
                    }

                    return list as IEnumerator<Student>;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving student" + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Student AddStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Operation_Failed_Duing + "added student" + Constants.Contact_System_Admin;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.CreateDT = DateTime.Now;
                        unitOfWork.Users.Add(student.User);
                        unitOfWork.Students.Add(student);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    return student;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "added student" + Constants.Contact_System_Admin;
                return null;
            }

        }
        public bool UpdateStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Operation_Failed_Duing + "updating student" + Constants.Contact_System_Admin;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(student.User, unitOfWork.Users.Get(student.UserSid));
                        Util.CopyNonNullProperty(student, unitOfWork.Students.Get(student.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "updating student" + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Operation_Failed_Duing + "deleting student" + Constants.Contact_System_Admin;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(student.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "deleting student" + Constants.Contact_System_Admin;
                return false;
            }
        }
        #endregion

        #region Instructor
        public Instructor GetActiveInstructorByInstructorSid(int sid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructor = unitOfWork.Instructors.Get(sid);
                    if (instructor != null)
                    {
                        instructor.User = unitOfWork.Users.Find(u => !u.DeleteDT.HasValue && u.Sid == instructor.UserSid).FirstOrDefault();
                        if (instructor.User == null)
                        {
                            message = "Instructor" + Constants.Not_Found;
                            return null;
                        }
                    }
                    else
                    {
                        message = "Instructor" + Constants.Not_Found;
                        return null;
                    }

                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving instructor" + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerator<Instructor> GetAllActiveInstructor(out string message)
        {
            throw new NotImplementedException();
        }
        public Instructor AddInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }
        public bool UpdateInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Admin
        public Admin AddAdmin(Admin admin, out string message)
        {
            throw new NotImplementedException();
        }
        public Admin GetActiveAdminByAdminSid(int sid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Admin> GetAllActiveAdmin(out string message)
        {
            throw new NotImplementedException();
        }
        public bool UpdateAdmin(Admin admin, out string message)
        {

            throw new NotImplementedException();
        }
        public bool DeleteAdmin(Admin admin, out string message)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Login
        /*
        *parameters: 
        *String userID
        *String password
        * 
        *Return user object, with respective admin, instructor and student list
        * 
        * check user.role
        * ActiveLearning.Business.Common.Constants
        * 
        * A = Admin
        * S = Student
        * I = Instructor
        */
        public User IsAuthenticated(string userID, string pass, out string messge)
        {
            // TODO, hash password and compare with DB 
            User authenticatedUser = null;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var user = unitOfWork.Users.SingleOrDefault(u => u.Username.Equals(userID, StringComparison.CurrentCultureIgnoreCase) && u.Password == pass && u.IsActive);

                    if (user != null)
                    {
                        switch (user.Role)
                        {
                            case Constants.User_Role_Student_Code:
                                var student = unitOfWork.Students.SingleOrDefault(s => s.UserSid == user.Sid);
                                if (student != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Students.Add(student);
                                }
                                messge = string.Empty;
                                break;
                            case Constants.User_Role_Instructor_Code:
                                var instructor = unitOfWork.Instructors.SingleOrDefault(i => i.UserSid == user.Sid);
                                if (instructor != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Instructors.Add(instructor);
                                }
                                messge = string.Empty;
                                break;

                            case Constants.User_Role_Admin_Code:
                                var admin = unitOfWork.Admins.SingleOrDefault(a => a.UserSid == user.Sid);
                                if (admin != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Admins.Add(admin);
                                }
                                messge = string.Empty;
                                break;

                            default:
                                messge = Constants.Invalid_Username_Or_Password;
                                return authenticatedUser;
                        }

                    }
                    else
                    {
                        messge = Constants.Invalid_Username_Or_Password;
                        return authenticatedUser;
                    }
                }
                return authenticatedUser;
            }
            catch (Exception ex)
            {
                messge = Constants.Operation_Failed_Duing + "anthenticating user" + Constants.Contact_System_Admin;
                return authenticatedUser;
            }
        }

        #endregion
    }
}
