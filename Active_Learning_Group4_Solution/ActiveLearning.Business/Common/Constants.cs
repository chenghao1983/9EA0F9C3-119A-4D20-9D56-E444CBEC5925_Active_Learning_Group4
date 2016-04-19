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
        public const string user_str = "User";
        public const string student_str = "Student";
        public const string EnrolledStudent_str = "Enrolled Student";
        public const string NonEnrolledStudent_str = "Non Enrolled Student";
        public const string instructor_str = "Instructor";
        public const string EnrolledInstructor_str = "Enrolled Instructor";

        public const string admin_str = "Admin";
        public const string userName_str = "User Name";
        public const string course_str = "Course";
        public const string courseName_str = "Course Name";
        public const string EnrolledCourse_str = "Enrolled Course";
        public const string NonEnrolledCourse_str = "Non Enrolled Course";
        public const string chat_str = "Chat";

        public const string Operation_Failed_Duing = "Operation failed during ";
        public const string Contact_System_Admin = ". Please contact system admin";
        public const string Invalid_Username_Or_Password = "Invalid Usernamd or Password";
        public const string Not_Found = " not found";
        public const string No = "No ";
        public const string Empty = "Empty ";
        public const string Already_Exists = " already exists";
        #endregion


        
    }
}
