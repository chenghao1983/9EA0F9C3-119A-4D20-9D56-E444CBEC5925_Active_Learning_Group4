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
        Student GetStudent(int sid);
        Student GetActiveStudent(int sid);
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);
        Instructor AddInstructor(Instructor instructor);
        Instructor GetInstructor(int sid);
        Instructor GetActiveInstructor(int sid);
        bool UpdateInstructor(Instructor instructor);
        bool DeleteInstructor(Instructor instructor);
        Admin AddAdmin(Admin admin);
        Admin GetAdmin(int sid);
        Admin GetActiveAdmin(int sid);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);
        User IsAuthenticated(string userID, string pass);
    }
}
