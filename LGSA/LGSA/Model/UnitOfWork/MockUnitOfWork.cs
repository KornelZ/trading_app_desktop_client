using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGSA.Model.Repositories;

namespace LGSA.Model.UnitOfWork
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private MainDatabaseEntities _context = new MainDatabaseEntities();
        public MainDatabaseEntities Context
        {
            get { return _context; }
        }

        public IRepository<users_Authetication> UsersRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<users_Authetication> AuthenticationRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<users> UserRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<product> ProductRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MockUnitOfWork()
        {
        }
        public void Commit()
        {
            
        }
        public void Rollback()
        {

        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void StartTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
