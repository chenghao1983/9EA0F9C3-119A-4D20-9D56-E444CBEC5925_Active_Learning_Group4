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

        public void AddInstructorAccount(User user)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {

                //User SU = new User();
                //SU.Username = user.LoginName;
                //SU.Password = user.Password;
                //SU.IsActive = true;

                db.Users.Add(user);
                db.SaveChanges();     

                Instructor instructor = new Instructor();
                foreach (var _user in db.Users)
                {
                    if (_user.Username == user.Username)
                    {
                        

                        instructor.Sid = _user.Sid;

                        db.Instructors.Add(instructor);

                        break;
                    }

                }
                db.SaveChanges();

            }
        }












    }
}

