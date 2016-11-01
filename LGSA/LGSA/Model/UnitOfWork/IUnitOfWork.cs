using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        MainDatabaseEntities Context { get; }
        void Commit();
        void Rollback();
        Task<int> Save();
    }
}
