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
        Student AddStudent(Student student);
        Student GetStudentBySid(int sid);
        Student GetActiveStudentBySid(int sid);
        IEnumerator<Student> GetAllStudent();
        IEnumerator<Student> GetAllActiveStudent();
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);
        Instructor AddInstructor(Instructor instructor);
        Instructor GetInstructorBySid(int sid);
        Instructor GetActiveInstructorBySid(int sid);
        IEnumerator<Instructor> GetAllInstructor(int pageSize, int pageNum);
        IEnumerator<Instructor> GetAllActiveInstructor(int pageSize, int pageNum);
        bool UpdateInstructor(Instructor instructor);
        bool DeleteInstructor(Instructor instructor);
        Admin AddAdmin(Admin admin);
        Admin GetAdminBySid(int sid);
        Admin GetActiveAdminBySid(int sid);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);
        User IsAuthenticated(string userID, string pass, out string messge);
    }
}
