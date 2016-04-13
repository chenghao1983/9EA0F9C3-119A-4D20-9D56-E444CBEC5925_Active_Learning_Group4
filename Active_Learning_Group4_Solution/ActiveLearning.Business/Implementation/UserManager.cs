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

namespace ActiveLearning.Business.Implementation
{
    public class UserManager : BaseManager, IUserManager
    {
        #region Student
        public Student GetStudent(int sid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var student = unitOfWork.Students.Get(sid);
                    student.User = unitOfWork.Users.Get(student.UserSid);
                    return student;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }

        public Student GetActiveStudent(int sid)
        {
            var student = GetStudent(sid);
            if (student.User == null || !student.User.IsActive || student.User.DeleteDT.HasValue)
            {
                return null;
            }
            return student;
        }
        public Student AddStudent(Student student)
        {
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
                return null;
            }
        }
        public bool UpdateStudent(Student student)
        {
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
                return false;
            }
        }
        public bool DeleteStudent(Student student)
        {
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
                return false;
            }
        }
        #endregion

        #region Instructor
        public Instructor GetInstructor(int sid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructor = unitOfWork.Instructors.Get(sid);
                    instructor.User = unitOfWork.Users.Get(instructor.UserSid);
                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }
        public Instructor GetActiveInstructor(int sid)
        {
            throw new NotImplementedException();
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    instructor.User.CreateDT = DateTime.Now;
                    unitOfWork.Users.Add(instructor.User);
                    unitOfWork.Instructors.Add(instructor);
                    unitOfWork.Complete();
                    scope.Complete();
                }
                return instructor;
            }

        }
        public bool UpdateInstructor(Instructor instructor)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(instructor.User, unitOfWork.Users.Get(instructor.UserSid));
                        Util.CopyNonNullProperty(instructor, unitOfWork.Students.Get(instructor.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return false;
            }
        }

        public bool DeleteInstructor(Instructor instructor)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(instructor.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return false;
            }
        }
        #endregion

        #region Admin
        public Admin GetAdmin(int sid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var admin = unitOfWork.Admins.Get(sid);
                    admin.User = unitOfWork.Users.Get(admin.UserSid);
                    return admin;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }
        public Admin GetActiveAdmin(int sid)
        {
            throw new NotImplementedException();
        }
        public Admin AddAdmin(Admin admin)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    admin.User.CreateDT = DateTime.Now;
                    unitOfWork.Users.Add(admin.User);
                    unitOfWork.Admins.Add(admin);
                    unitOfWork.Complete();
                    scope.Complete();
                }
                return admin;
            }
        }
        public bool UpdateAdmin(Admin admin)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(admin.User, unitOfWork.Users.Get(admin.UserSid));
                        Util.CopyNonNullProperty(admin, unitOfWork.Students.Get(admin.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return false;
            }
        }
        public bool DeleteAdmin(Admin admin)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(admin.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return false;
            }
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
        public User IsAuthenticated(string userID, string pass)
        {
            User authenticatedUser = null;

            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var user = unitOfWork.Users.SingleOrDefault(u => u.Username.Equals(userID, StringComparison.CurrentCultureIgnoreCase) && u.Password == pass);

                if (user != null)
                {
                    switch (user.Role)
                    {
                        case Constants.User_Role_Student_Code:
                            var student = unitOfWork.Students.SingleOrDefault(s => s.UserSid == user.Sid);
                            if (student != null)
                            {
                                authenticatedUser = new User();
                                authenticatedUser.Username = userID;
                                authenticatedUser.Password = pass;
                                authenticatedUser.Students.Add(student);
                            }

                            break;
                        case Constants.User_Role_Instructor_Code:
                            var instructor = unitOfWork.Instructors.SingleOrDefault(i => i.UserSid == user.Sid);
                            if (instructor != null)
                            {
                                authenticatedUser = new User();
                                authenticatedUser.Username = userID;
                                authenticatedUser.Password = pass;
                                authenticatedUser.Instructors.Add(instructor);
                            }
                            break;

                        case Constants.User_Role_Admin_Code:
                            var admin = unitOfWork.Admins.SingleOrDefault(a => a.UserSid == user.Sid);
                            if (admin != null)
                            {
                                authenticatedUser = new User();
                                authenticatedUser.Username = userID;
                                authenticatedUser.Password = pass;
                                authenticatedUser.Admins.Add(admin);
                            }
                            break;

                        default:
                            return authenticatedUser;
                    }

                }
                else
                {
                    return authenticatedUser;
                }
                return null;
            }
        }

      
       
        #endregion
    }
}
