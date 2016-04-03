using ActiveLearning.DB;
using ActiveLearning.Entities.ViewModel;
using ActiveLearning.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Repository
{
    public class UserRepository : Repository<UserViewModel>, IUserRepository
    {
        public UserRepository(ENET_Project_Active_Learning_Group4Entities context) : base(context)
        {
        }

        public void AddUserAccount(UserViewModel user)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {

                Admin SU = new Admin();
                SU.Username = user.LoginName;
                SU.Password = user.Password;
                SU.Sid = 1;

                db.Admins.Add(SU);
                db.SaveChanges();


            }
        }












    }
}

