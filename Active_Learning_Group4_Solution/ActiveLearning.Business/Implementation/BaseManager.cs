using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Implementation
{
    public class BaseManager : IDisposable
    {
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
        public const string chat_str = "Chat";

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


        public void ErrorLog(string error)
        {
            //TODO call log4net
        }

        public void ErrorLog(Exception ex)
        {
            //TODO call log4net
        }

        public void WarningLog(string warning)
        {
            //TODO call log4net
        }

        public void InfoLog(string info)
        {
            //TODO call log4net
        }
    }
}
