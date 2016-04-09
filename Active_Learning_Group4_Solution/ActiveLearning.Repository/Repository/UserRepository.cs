using ActiveLearning.DB;
using ActiveLearning.Entities.ViewModel;
using ActiveLearning.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
                using (TransactionScope scope = new TransactionScope())
                {
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

                    scope.Complete();
                }
            }
        }

        public void AddStudentAccount(User user)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    Student student = new Student();
                    foreach (var _user in db.Users)
                    {
                        if (_user.Username == user.Username)
                        {


                            student.Sid = _user.Sid;

                            db.Students.Add(student);

                            break;
                        }

                    }

                    db.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public void CheckAuthentication(User user)
        {
            using (ENET_Project_Active_Learning_Group4Entities db = new ENET_Project_Active_Learning_Group4Entities())
            {
                var _user = db.Users.SingleOrDefault(a => a.Username == user.Username && a.Password == user.Password);

                if (_user != null)
                {
                    if (db.Students.SingleOrDefault(a => a.Sid == _user.Sid) != null)
                    {
                       // Student exists
                    }
                    else if (db.Instructors.SingleOrDefault(a => a.Sid == _user.Sid) != null)
                    {
                        // Instructor exists
                    }


                }


            }
        }








    }
}

