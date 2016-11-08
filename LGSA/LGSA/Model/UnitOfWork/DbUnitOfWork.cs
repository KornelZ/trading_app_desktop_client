using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGSA.Model.Repositories;

namespace LGSA.Model.UnitOfWork
{
    public class DbUnitOfWork : IUnitOfWork
    {
        private MainDatabaseEntities _context;
        private DbContextTransaction _transaction;
        private IRepository<users_Authetication> _authenticationRepository;
        private IRepository<product> _productRepository;
        private IRepository<buy_Offer> _buyOfferRepository;
        private IRepository<dic_condition> _conditionRepository;
        private IRepository<dic_Genre> _genreRepository;
        private IRepository<dic_Product_type> _productTypeRepository;
        private IRepository<sell_Offer> _sellOfferRepository;
        public MainDatabaseEntities Context
        {
            get { return _context; }
        }

        public IRepository<users_Authetication> AuthenticationRepository
        {
            get { return _authenticationRepository; }
        }

        public IRepository<product> ProductRepository
        {
            get { return _productRepository; }
        }
        public IRepository<buy_Offer> BuyOfferRepository
        {
            get { return _buyOfferRepository; }
        }
        public IRepository<sell_Offer> SellOfferRepository
        {
            get { return _sellOfferRepository; }
        }
        public IRepository<dic_condition> ConditionRepository
        {
            get { return _conditionRepository; }
        }
        public IRepository<dic_Genre> GenreRepository
        {
            get { return _genreRepository; }
        }
        public IRepository<dic_Product_type> ProductTypeRepository
        {
            get { return _productTypeRepository; }
        }

        public DbUnitOfWork()
        {
            _context = new MainDatabaseEntities();
            _authenticationRepository = new AuthenticationRepository(_context);
            _productRepository = new Repository<product>(_context);
            _buyOfferRepository = new BuyOfferRepository(_context);
            _sellOfferRepository = new SellOfferRepository(_context);
            _conditionRepository = new Repository<dic_condition>(_context);
            _genreRepository = new Repository<dic_Genre>(_context);
            _productTypeRepository = new Repository<dic_Product_type>(_context);
        }
        public void Commit()
        {
            _transaction?.Commit();
        }
        public void Rollback()
        {
            _transaction?.Rollback();
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }  
        public async Task<int> Save()
        {
            return  await _context.SaveChangesAsync();
        }

        public void StartTransaction()
        {
            if(_transaction == null)
            {
                _transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            }
        }
    }
}
