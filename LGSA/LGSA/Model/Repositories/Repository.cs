using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }
        public virtual bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> filter)
        {
            if(filter == null)
            {
                return await _context.Set<T>().ToListAsync();
            }
            
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public virtual bool Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
