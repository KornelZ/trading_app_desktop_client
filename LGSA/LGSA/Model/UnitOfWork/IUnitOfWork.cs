using LGSA.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<users_Authetication> AuthenticationRepository { get; }
        void StartTransaction();
        void Commit();
        void Rollback();
        Task<int> Save();
    }
}
