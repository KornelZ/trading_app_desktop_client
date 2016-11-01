using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public class DbUnitOfWork : IUnitOfWork
    {
        private MainDatabaseEntities _context;
        private DbContextTransaction _transaction;
        public MainDatabaseEntities Context
        {
            get { return _context; }
        }
        public DbUnitOfWork()
        {
            _context = new MainDatabaseEntities();
            _transaction = _context.Database.BeginTransaction();
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
            return await _context.SaveChangesAsync();
        }
    }
}
