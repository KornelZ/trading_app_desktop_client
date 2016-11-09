using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> filter);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
