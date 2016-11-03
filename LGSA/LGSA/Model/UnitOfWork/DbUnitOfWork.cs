using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGSA.Model.Repositories;

namespace LGSA.Model.UnitOfWork
{
    public class DbUnitOfWork : IUnitOfWork
    {
        private MainDatabaseEntities _context;
        private DbContextTransaction _transaction;
        private IRepository<users_Authetication> _authenticationRepository;
        private IRepository<users> _userRepository;      
        public MainDatabaseEntities Context
        {
            get { return _context; }
        }

        public IRepository<users_Authetication> AuthenticationRepository
        {
            get { return _authenticationRepository; }
        }
        public IRepository<users> UserRepository
        {
            get { return _userRepository; }
        }
        public DbUnitOfWork()
        {
            _context = new MainDatabaseEntities();
            _authenticationRepository = new AuthenticationRepository(_context);
            _userRepository = new UserRepository(_context);
            _transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
            }
            if(_context != null)
            {
                _context.Dispose();
            }
        }  
        public async Task<int> Save()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
