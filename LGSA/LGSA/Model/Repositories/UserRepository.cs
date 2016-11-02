using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class UserRepository : IRepository<users>
    {
        private MainDatabaseEntities _context;
        public UserRepository(MainDatabaseEntities context)
        {
            _context = context;
        }
        public bool Add(users entity)
        {
            if(_context.users.Any(u => u.Last_Name == entity.Last_Name &&
            u.First_Name == entity.First_Name))
            {
                return false;
            }
            _context.users.Add(entity);
            return true;
        }

        public void Delete(users entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<users> GetById(int id)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<IEnumerable<users>> GetData()
        {
            return await _context.users.Take(100).ToListAsync();
        }

        public bool Update(users entity)
        {
            if (_context.users.Any(u => u.Last_Name == entity.Last_Name &&
            u.First_Name == entity.First_Name))
            {
                return false;
            }
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
