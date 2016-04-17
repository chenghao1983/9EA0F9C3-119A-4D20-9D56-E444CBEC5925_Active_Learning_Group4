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
        public const string Operation_Failed_Duing = "Operation failed during ";
        public const string Contact_System_Admin = ". Please contact system admin";
        public const string Invalid_Username_Or_Password = "Invalid Usernamd or Password";
        public const string Not_Found = " not found";
        public const string No = "No ";
        #endregion

    }
}
