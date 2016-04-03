using ActiveLearning.DB;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ENET_Project_Active_Learning_Group4Entities _context;

        public UnitOfWork(ENET_Project_Active_Learning_Group4Entities context)
        {
            _context = context;
            Users = new UserRepository(_context);

        }

        public IUserRepository Users { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
