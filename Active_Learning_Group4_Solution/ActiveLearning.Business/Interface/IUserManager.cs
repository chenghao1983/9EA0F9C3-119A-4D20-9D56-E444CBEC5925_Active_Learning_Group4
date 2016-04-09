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

        void AddNewStudent(User user);

        void AddNewInstructor(User user);

        void AddNewAdmin();

        void isAuthenticated(User user);




    }
}
