using LGSA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class AuthenticationRepository : Repository<users_Authetication>
    {
        public AuthenticationRepository(DbContext context) : base(context)
        {
        }

        public override bool Add(users_Authetication entity)
        {
            if(_context.Set<users_Authetication>()
                .Include(users_Authetication => users_Authetication.users1)
                .Any(u => u.users1.First_Name == entity.users1.First_Name &&
                u.users1.Last_Name == entity.users1.Last_Name))
            {
                return false;
            }
            return base.Add(entity);
        }

    }
}
