using LGSA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LGSA.Model.Repositories
{
    public class AuthenticationRepository : Repository<users_Authetication>
    {
        public AuthenticationRepository(DbContext context) : base(context)
        {
        }

        public override users_Authetication Add(users_Authetication entity)
        {
            if(_context.Set<users_Authetication>()
                .Include(users_Authetication => users_Authetication.users1)
                .Any(u => u.users1.First_Name == entity.users1.First_Name &&
                u.users1.Last_Name == entity.users1.Last_Name))
            {
                return null;
            }
            return base.Add(entity);
        }
        public override async Task<IEnumerable<users_Authetication>> GetData(Expression<Func<users_Authetication, bool>> filter)
        {
            if (filter == null)
            {
                return await _context.Set<users_Authetication>().Include(u => u.users1).ToListAsync();
            }

            return await _context.Set<users_Authetication>().Include(u => u.users1).Where(filter).ToListAsync();
        }

    }
}
