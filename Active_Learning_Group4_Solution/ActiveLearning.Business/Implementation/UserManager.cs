using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ActiveLearning.Repository.Context;
using System.Transactions;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Business.Implementation
{
    public class UserManager : BaseManager, IUserManager
    {
        #region user
        public bool UserNameExists(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName))
            {
                message = Constants.Empty + Constants.userName_str;
                return true;
            }
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var user = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.DeleteDT.HasValue).SingleOrDefault();
                if (user != null)
                {
                    message = userName + Constants.Already_Exists;
                    return true;
                }
            }
            message = string.Empty;
            return false;
        }
        public User GenerateHashedUser(User user, out string message)
        {
            if (user == null)
            {
                message = Constants.Empty + Constants.user_str;
                return null;
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                message = Constants.Empty + Constants.userName_str;
                return null;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                message = Constants.Empty + Constants.Password_str;
                return null;
            }
            try
            {
                string salt = Util.GenerateSalt();
                user.PasswordSalt = salt;
                user.Password = Util.CreateHash(user.Password, salt);
                message = string.Empty;
                return user;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "adding " + Constants.user_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        #endregion

        #region Student
        public Student GetStudentByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var student = unitOfWork.Students.Get(studentSid);

                    if (student == null)
                    {
                        message = Constants.student_str + Constants.Not_Found;
                        return null;
                    }

                    student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();

                    if (student.User == null)
                    {
                        message = Constants.student_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return student;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + Constants.student_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Student GetActiveStudentByStudentSid(int studentSid, out string message)
        {
            var student = GetStudentByStudentSid(studentSid, out message);
            if (student == null || student.User == null)
            {
                return null;
            }
            if (!student.User.IsActive)
            {
                return null;
            }
            return student;
        }
        public IEnumerable<Student> GetAllStudent(out string message)
        {
            message = string.Empty;
            List<Student> list = new List<Student>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var students = unitOfWork.Students.GetAll();
                    if (students == null || students.Count() == 0)
                    {
                        message = Constants.No + Constants.student_str;
                        return null;
                    }

                    foreach (var student in students)
                    {
                        student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();
                        if (student.User != null)
                        {
                            list.Add(student);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.No + Constants.student_str;
                        return null;
                    }
                    message = string.Empty;
                    return list;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + Constants.student_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<Student> GetAllActiveStudent(out string message)
        {
            var allStudents = GetAllStudent(out message);
            if (allStudents == null || allStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return allStudents.Where(s => s.User.IsActive);
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Student AddStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Empty + Constants.student_str;
                return null;
            }
            if (UserNameExists(student.User.Username, out message))
            {
                return null;
            }
            var hasedUser = GenerateHashedUser(student.User, out message);
            if (hasedUser == null)
            {
                return null;
            }
            student.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.CreateDT = DateTime.Now;
                        unitOfWork.Students.Add(student);
                        unitOfWork.Users.Add(student.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return student;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "adding " + Constants.student_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Empty + Constants.student_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(student, unitOfWork.Students.Get(student.Sid));
                        Util.CopyNonNullProperty(student.User, unitOfWork.Users.Get(student.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "updating " + Constants.student_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.Empty + Constants.student_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.DeleteDT = DateTime.Now;
                        Util.CopyNonNullProperty(student.User, unitOfWork.Users.Get(student.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "deleting " + Constants.student_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        #endregion

        #region Instructor
        public Instructor GetInstructorByInstructorSid(int instructorSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructor = unitOfWork.Instructors.Get(instructorSid);

                    if (instructor == null)
                    {
                        message = Constants.instructor_str + Constants.Not_Found;
                        return null;
                    }

                    instructor.User = unitOfWork.Users.Find(i => i.Sid == instructor.UserSid && !i.DeleteDT.HasValue).FirstOrDefault();

                    if (instructor.User == null)
                    {
                        message = Constants.instructor_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving" + Constants.instructor_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Instructor GetActiveInstructorByInstructorSid(int instructorSid, out string message)
        {
            var instructor = GetInstructorByInstructorSid(instructorSid, out message);
            if (instructor == null || instructor.User == null)
            {
                return null;
            }
            if (!instructor.User.IsActive)
            {
                return null;
            }
            return instructor;
        }
        public IEnumerable<Instructor> GetAllActiveInstructor(out string message)
        {
            message = string.Empty;
            List<Instructor> list = new List<Instructor>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructors = unitOfWork.Instructors.GetAll();
                    if (instructors == null || instructors.Count() == 0)
                    {
                        message = Constants.No + Constants.instructor_str;
                        return null;
                    }

                    foreach (var instructor in instructors)
                    {
                        instructor.User = unitOfWork.Users.Find(u => u.Sid == instructor.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();
                        if (instructor.User != null)
                        {
                            list.Add(instructor);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.No + Constants.instructor_str;
                        return null;
                    }
                    message = string.Empty;
                    return list;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + Constants.instructor_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Instructor AddInstructor(Instructor instructor, out string message)
        {
            message = string.Empty;
            if (instructor == null || instructor.User == null)
            {
                message = Constants.Empty + Constants.instructor_str;
                return null;
            }
            if (UserNameExists(instructor.User.Username, out message))
            {
                return null;
            }
            var hasedUser = GenerateHashedUser(instructor.User, out message);
            if (hasedUser == null)
            {
                return null;
            }
            instructor.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.CreateDT = DateTime.Now;
                        unitOfWork.Instructors.Add(instructor);
                        unitOfWork.Users.Add(instructor.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "adding " + Constants.instructor_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateInstructor(Instructor instructor, out string message)
        {
            message = string.Empty;
            if (instructor == null || instructor.User == null)
            {
                message = Constants.Empty + Constants.instructor_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(instructor, unitOfWork.Instructors.Get(instructor.Sid));
                        Util.CopyNonNullProperty(instructor.User, unitOfWork.Users.Get(instructor.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "updating " + Constants.instructor_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteInstructor(Instructor instructor, out string message)
        {
            message = string.Empty;
            if (instructor == null || instructor.User == null)
            {
                message = Constants.Empty + Constants.instructor_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.DeleteDT = DateTime.Now;
                        Util.CopyNonNullProperty(instructor.User, unitOfWork.Users.Get(instructor.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "deleting " + Constants.instructor_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        #endregion

        #region Admin
        public Admin GetAdminByAdminSid(int adminSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var admin = unitOfWork.Admins.Get(adminSid);

                    if (admin == null)
                    {
                        message = Constants.admin_str + Constants.Not_Found;
                        return null;
                    }

                    admin.User = unitOfWork.Users.Find(u => u.Sid == admin.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();

                    if (admin.User == null)
                    {
                        message = Constants.admin_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return admin;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + Constants.admin_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Admin GetActiveAdminByAdminSid(int adminSid, out string message)
        {
            var admin = GetAdminByAdminSid(adminSid, out message);
            if (admin == null || admin.User == null)
            {
                return null;
            }
            if (!admin.User.IsActive)
            {
                return null;
            }
            return admin;
        }
        public IEnumerable<Admin> GetAllActiveAdmin(out string message)
        {
            message = string.Empty;
            List<Admin> list = new List<Admin>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var admins = unitOfWork.Admins.GetAll();
                    if (admins == null || admins.Count() == 0)
                    {
                        message = Constants.No + Constants.admin_str;
                        return null;
                    }

                    foreach (var admin in admins)
                    {
                        admin.User = unitOfWork.Users.Find(u => u.Sid == admin.UserSid && !u.DeleteDT.HasValue).SingleOrDefault();
                        if (admin.User != null)
                        {
                            list.Add(admin);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.No + Constants.admin_str;
                        return null;
                    }
                    message = string.Empty;
                    return list;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "retrieving " + Constants.admin_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        /*
        *Will check username
        *If username exists, will return null and username already exists message 
       */
        public Admin AddAdmin(Admin admin, out string message)
        {
            message = string.Empty;
            if (admin == null || admin.User == null)
            {
                message = Constants.Empty + Constants.admin_str;
                return null;
            }
            if (UserNameExists(admin.User.Username, out message))
            {
                return null;
            }
            var hasedUser = GenerateHashedUser(admin.User, out message);
            if (hasedUser == null)
            {
                return null;
            }
            admin.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.CreateDT = DateTime.Now;
                        unitOfWork.Admins.Add(admin);
                        unitOfWork.Users.Add(admin.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return admin;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "adding " + Constants.admin_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateAdmin(Admin admin, out string message)
        {
            message = string.Empty;
            if (admin == null || admin.User == null)
            {
                message = Constants.Empty + Constants.admin_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(admin, unitOfWork.Admins.Get(admin.Sid));
                        Util.CopyNonNullProperty(admin.User, unitOfWork.Users.Get(admin.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "updating " + Constants.admin_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteAdmin(Admin admin, out string message)
        {
            message = string.Empty;
            if (admin == null || admin.User == null)
            {
                message = Constants.Empty + Constants.admin_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.DeleteDT = DateTime.Now;
                        Util.CopyNonNullProperty(admin.User, unitOfWork.Users.Get(admin.User.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + "deleting " + Constants.admin_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        #endregion

        #region Login
        /*
        *parameters: 
        *String userName
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
        public User IsAuthenticated(string userName, string pass, out string messge)
        {
            // TODO, hash password and compare with DB 
            User authenticatedUser = null;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var user = unitOfWork.Users.SingleOrDefault(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.IsActive && !u.DeleteDT.HasValue);

                    if (user != null)
                    {
                        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.PasswordSalt))
                        {
                            messge = user_str + "corrupted" + Constants.Contact_System_Admin;
                            return null;
                        }

                        if (!Util.ValidatePassword(pass, user.Password, user.PasswordSalt))
                        {
                            messge = Constants.Invalid_Username_Or_Password;
                            return authenticatedUser;
                        }
                        InfoLog("User: " + user.Username + " authenticated");
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
                ExceptionLog(ex);
                return authenticatedUser;
            }
        }
        public User IsAuthenticated(User user, out string message)
        {
            if (user == null)
            {
                message = Constants.Empty + Constants.user_str;
                return null;
            }
            return IsAuthenticated(user.Username, user.Password, out message);
        }
        #endregion
    }
}
