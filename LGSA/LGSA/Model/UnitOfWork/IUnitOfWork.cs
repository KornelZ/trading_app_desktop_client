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
        IRepository<product> ProductRepository { get; }
        IRepository<buy_Offer> BuyOfferRepository { get; }
        IRepository<dic_condition> ConditionRepository { get; }
        IRepository<dic_Genre> GenreRepository { get; }
        IRepository<dic_Product_type> ProductTypeRepository { get; }
        void StartTransaction();
        void Commit();
        void Rollback();
        Task<int> Save();
    }
}
