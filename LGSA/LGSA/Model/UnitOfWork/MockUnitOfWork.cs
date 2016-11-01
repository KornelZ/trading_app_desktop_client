using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private MainDatabaseEntities _context = new MainDatabaseEntities();
        private DbContextTransaction _transaction;
        public MainDatabaseEntities Context
        {
            get { return _context; }
        }
        public MockUnitOfWork()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Rollback();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_context != null)
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
