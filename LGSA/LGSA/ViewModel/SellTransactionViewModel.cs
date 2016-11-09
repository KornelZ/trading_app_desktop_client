using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Model.Services;
using LGSA.Model.UnitOfWork;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.ViewModel
{
    public class SellTransactionViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private FilterViewModel _filter;
        private SellOfferService _sellOfferService;
        private ProductService _productService;
        private TransactionService _transactionService;

        private SellOfferWrapper _selectedOffer;
        private BindableCollection<SellOfferWrapper> _offers;
        private AsyncRelayCommand _acceptCommand;

        public SellTransactionViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _sellOfferService = new SellOfferService(factory);
            _productService = new ProductService(factory);
            _transactionService = new TransactionService(factory);
            _user = user;

            _filter = filter;
            Offers = new BindableCollection<SellOfferWrapper>();
            SelectedOffer = new SellOfferWrapper(new sell_Offer());

            AcceptCommand = new AsyncRelayCommand(execute => Accept(), canExecute => { return true; });
        }

        public AsyncRelayCommand AcceptCommand
        {
            get { return _acceptCommand; }
            set { _acceptCommand = value; Notify(); }
        }
        public SellOfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value; Notify(); }
        }
        public BindableCollection<SellOfferWrapper> Offers
        {
            get { return _offers; }
            set { _offers = value; Notify(); }
        }
        public async Task Load()
        {
            var offers = await _sellOfferService.GetData(CreateFilter());
            Offers.Clear();

            foreach (var offer in offers)
            {
                Offers.Add(SellOfferWrapper.CreateSellOffer(offer));
            }
        }

        private Expression<Func<sell_Offer, bool>> CreateFilter()
        {
            String genre = "";
            String conditon = "";
            int rating = 1;
            int stock = 1;
            double price = 1;
            if (!_filter.Condition.Name.Equals("All/Any"))
            {
                conditon = _filter.Condition.Name;
            }
            if (!_filter.Genre.Name.Equals("All/Any"))
            {
                genre = _filter.Genre.Name;
            }
            if (_filter.Rating != null)
            {
                rating = int.Parse(_filter.Rating);
            }
            if (_filter.Price != null)
            {
                price = double.Parse(_filter.Price);
            }
            if (_filter.Stock != null)
            {
                stock = int.Parse(_filter.Stock);
            }

            Expression<Func<sell_Offer, bool>> filter = b => b.seller_id != _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }

        public async Task Accept()
        {
            product buyerProduct = null;
            var productQuery = await _productService.GetData(prod => prod.product_owner == _user.Id && prod.Name == SelectedOffer.Product.Name);
            if (productQuery.Count() != 0)
            {
                buyerProduct = productQuery.First();
                NullProductProperties(buyerProduct);
            }
            else
            {
                buyerProduct = new product()
                {
                    product_owner = _user.Id,
                    Name = SelectedOffer.Product.Name,
                    condition_id = SelectedOffer.Product.ConditionId,
                    genre_id = SelectedOffer.Product.GenreId,
                    rating = SelectedOffer.Product.Rating,
                    sold_copies = SelectedOffer.Product.SoldCopies,
                    product_type_id = SelectedOffer.Product.ProductTypeId,
                    Update_Date = DateTime.Now,
                    Update_Who = _user.Id
                };
            }

            SelectedOffer.StatusId = (int)TransactionState.Finished;
            var buyOffer = new buy_Offer()
            {
                name = SelectedOffer.Name,
                Update_Who = _user.Id,
                Update_Date = DateTime.Now,
                buyer_id = _user.Id,
                status_id = (int)TransactionState.Finished,
                price = (double)SelectedOffer.Price,
                amount = SelectedOffer.Amount,
            };
            SelectedOffer.NullNavigationProperties();
            SelectedOffer.Product.NullNavigationProperties();
            bool result = await _transactionService.AcceptSellTransaction(SelectedOffer.SellOffer, buyOffer,
                buyerProduct, SelectedOffer.Product.Product);

            Offers.Remove(SelectedOffer);
        }

        private void NullProductProperties(product buyerProduct)
        {
            buyerProduct.dic_condition = null;
            buyerProduct.dic_Genre = null;
            buyerProduct.dic_Product_type = null;
            buyerProduct.users = null;
            buyerProduct.users1 = null;
        }
    }
}
