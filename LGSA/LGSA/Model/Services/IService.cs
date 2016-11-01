using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetData();
        Task<bool> Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
