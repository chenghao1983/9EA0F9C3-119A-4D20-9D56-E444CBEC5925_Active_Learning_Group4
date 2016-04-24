using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ActiveLearning.Repository.Context;
using System.Transactions;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Business.Mock
{
   public class MockUserManager: IUserManager, IManagerFactoryBase<IUserManager>
    {

        public IUserManager Create()
        {
            return this;
        }

        public void Dispose()
        {

        }

        #region user
        public bool UserNameExists(string userName, out string message)
        {
            throw new NotImplementedException();
        }
        public User GenerateHashedUser(User user, out string message)
        {
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
        public User IsAuthenticated(User user, out string message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Student
        public Student GetStudentByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public Student GetActiveStudentByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Student> GetAllStudent(out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Student> GetAllActiveStudent(out string message)
        {
            throw new NotImplementedException();
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Student AddStudent(Student student, out string message)
        {
            throw new NotImplementedException();
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateStudent(Student student, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteStudent(Student student, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteStudent(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool ActivateStudent(Student student, out string message)
        {
            throw new NotImplementedException();
        }
        public bool ActivateStudent(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeactivateStudent(Student student, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeactivateStudent(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Instructor
        public Instructor GetInstructorByInstructorSid(int instructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public Instructor GetActiveInstructorByInstructorSid(int instructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Instructor> GetAllActiveInstructor(out string message)
        {
            throw new NotImplementedException();
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Instructor AddInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteInstructor(int instructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool ActivateInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }

        public bool ActivateInstructor(int instructorSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateInstructor(Instructor instructor, out string message)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateInstructor(int instructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Admin
        public Admin GetAdminByAdminSid(int adminSid, out string message)
        {
            throw new NotImplementedException();
        }
        public Admin GetActiveAdminByAdminSid(int adminSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Admin> GetAllActiveAdmin(out string message)
        {
            throw new NotImplementedException();
        }
        /*
        *Will check username
        *If username exists, will return null and username already exists message 
       */
        public Admin AddAdmin(Admin admin, out string message)
        {
            throw new NotImplementedException();
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateAdmin(Admin admin, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteAdmin(Admin admin, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteAdmin(int adminSid, out string message)
        {
            throw new NotImplementedException();

        }


        #endregion

    }
}
