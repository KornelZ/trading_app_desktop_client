using LGSA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class AuthenticationRepository : IRepository<users_Authetication>
    {
        private MainDatabaseEntities _context;
        public AuthenticationRepository(MainDatabaseEntities context)
        {
            _context = context;
        }

        public bool Add(users_Authetication entity)
        {
            if(entity.users1 != null)
            {
                if(_context.users.Any(u => u.First_Name == entity.users1.First_Name && u.Last_Name == entity.users1.Last_Name))
                {
                    return false;
                }
            }
            _context.users_Authetication.Add(entity);

            return true;
        }

        public void Delete(users_Authetication entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<users_Authetication> GetById(int id)
        {
            return await _context.users_Authetication.FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<IEnumerable<users_Authetication>> GetData()
        {
            return await _context.users_Authetication.Take(100).ToListAsync();
        }

        public bool Update(users_Authetication entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
