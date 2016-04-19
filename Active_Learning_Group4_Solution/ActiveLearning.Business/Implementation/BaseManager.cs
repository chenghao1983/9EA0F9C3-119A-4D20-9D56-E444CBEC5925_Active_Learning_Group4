using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using log4net;

namespace ActiveLearning.Business.Implementation
{
    public class BaseManager : IDisposable
    {
        protected const string LF = "\r\n";
        private const string SEPARATOR = "---------------------------------------------------------------------------------------------------------------";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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


        public static void ExceptionLog(Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void ExceptionLog(string userLog, Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(userLog);
                sb.Append(LF);
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void InfoLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsInfoEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Error(message);
            }
        }

        public static void DebugLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }

        public static void ExceptionLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }
    }
}
