using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Common
{
    public class Constants
    {
        #region User Role

        public const string User_Role_Student_Code = "S";
        public const string User_Role_Instructor_Code = "I";
        public const string User_Role_Admin_Code = "A";

        //public const string User_Role_Student_Name = "Student";
        //public const string User_Role_Instructor_Name = "Instructor";
        //public const string User_Role_Admin_Name = "Admin";
        #endregion

        #region Error Message
        public const string User = "User";
        public const string Student = "Student";
        public const string EnrolledStudent = "Enrolled Student";
        public const string NonEnrolledStudent = "Non Enrolled Student";
        public const string Instructor = "Instructor";
        public const string EnrolledInstructor = "Enrolled Instructor";
        public const string NonEnrolledInstructor = "Non Enrolled Instructor";
        public const string Student_Course_Enrolment = "Student Course Enrolment ";
        public const string Instructor_Course_Enrolment = "Instructor Course Enrolment ";

        public const string Admin = "Admin";
        public const string UserName = "User Name";
        public const string Password = "Password";
        public const string Course = "Course";
        public const string CourseName = "Course Name";
        public const string EnrolledCourse = "Enrolled Course";
        public const string NonEnrolledCourse = "Non Enrolled Course";
        public const string Chat = "Chat";
        public const string Quiz = "Quiz";
        public const string QuizTitle = "Quiz Title";

        public const string Operation_Failed_Duing = "Operation failed during ";
        public const string Contact_System_Admin = ". Please contact system admin";
        public const string Invalid_Username_Or_Password = "Invalid Username or Password";
        public const string Not_Found = " not found";
        public const string No = "No ";
        public const string Empty = "Empty ";
        public const string Updating = "Updating ";
        public const string Saving = "Saving ";
        public const string Retrieving = "Retrieving ";
        public const string Adding = "Adding ";
        public const string Deleting = "Deleting ";
        public const string Already_Exists = " already exists";

        public const string Authenticating_User = "Authenticating User ";
        public const string Authenticated = "Authenticated ";
        public static string Corrupted = "Corrupted";

        #endregion



    }
}
