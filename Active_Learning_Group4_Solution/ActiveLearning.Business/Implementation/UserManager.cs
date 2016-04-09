using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Implementation
{
    public class UserManager : IUserManager
    {
        public void AddNewAdmin()
        {
            throw new NotImplementedException();
        }

        public void AddNewInstructor(User user)
        {
            using (var unitOfWork = new UnitOfWork(new ENET_Project_Active_Learning_Group4Entities()))
            {
                unitOfWork.Users.AddInstructorAccount(user);

                unitOfWork.Complete();
            }
        }

        public void AddNewStudent(User user)
        {
            using (var unitOfWork = new UnitOfWork(new ENET_Project_Active_Learning_Group4Entities()))
            {
                unitOfWork.Users.AddStudentAccount(user);

                unitOfWork.Complete();
            }
        }

        public void isAuthenticated(User user)
        {
            using (var unitOfWork = new UnitOfWork(new ENET_Project_Active_Learning_Group4Entities()))
            {
                unitOfWork.Users.CheckAuthentication(user);

                unitOfWork.Complete();
            }
        }
    }
}
