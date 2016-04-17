using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface IUserManager
    {
        bool UserNameExists(string userName, out string message);
        Student GetStudentByStudentSid(int studentSid, out string message);
        Student GetActiveStudentByStudentSid(int studentSid, out string message);
        IEnumerator<Student> GetAllActiveStudent(out string message);
        Student AddStudent(Student student, out string message);
        bool UpdateStudent(Student student, out string message);
        bool DeleteStudent(Student student, out string message);
        Instructor GetInstructorByInstructorSid(int instructorSid, out string message);
        Instructor GetActiveInstructorByInstructorSid(int instructorSid, out string message);
        IEnumerator<Instructor> GetAllActiveInstructor( out string message);
        Instructor AddInstructor(Instructor instructor, out string message);
        bool UpdateInstructor(Instructor instructor, out string message);
        bool DeleteInstructor(Instructor instructor, out string message);
        Admin GetAdminByAdminSid(int adminSid, out string message);
        Admin GetActiveAdminByAdminSid(int adminSid, out string message);
        IEnumerator<Admin> GetAllActiveAdmin(out string message);
        Admin AddAdmin(Admin admin, out string message);
        bool UpdateAdmin(Admin admin, out string message);
        bool DeleteAdmin(Admin admin, out string message);
        User IsAuthenticated(string userName, string password, out string messge);
    }
}
