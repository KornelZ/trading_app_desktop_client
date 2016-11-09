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
using System.Windows;

namespace LGSA.ViewModel
{
    public class BuyTransactionViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private FilterViewModel _filter;
        private BuyOfferService _buyOfferService;
        private ProductService _productService;
        private TransactionService _transactionService;

        private BuyOfferWrapper _selectedOffer;
        private BindableCollection<BuyOfferWrapper> _offers;
        private AsyncRelayCommand _acceptCommand;

        public BuyTransactionViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _buyOfferService = new BuyOfferService(factory);
            _productService = new ProductService(factory);
            _transactionService = new TransactionService(factory);
            _user = user;

            _filter = filter;
            Offers = new BindableCollection<BuyOfferWrapper>();
            SelectedOffer = new BuyOfferWrapper(new buy_Offer());

            AcceptCommand = new AsyncRelayCommand(execute => Accept(), canExecute => { return true; });
        }

        public AsyncRelayCommand AcceptCommand
        {
            get { return _acceptCommand; }
            set { _acceptCommand = value; Notify(); }
        }
        public BuyOfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value; Notify(); }
        }
        public BindableCollection<BuyOfferWrapper> Offers
        {
            get { return _offers; }
            set { _offers = value; Notify(); }
        }
        public async Task Load()
        {
            var offers = await _buyOfferService.GetData(CreateFilter());
            Offers.Clear();

            foreach(var offer in offers)
            {
                Offers.Add(BuyOfferWrapper.CreateBuyOffer(offer));
            }
        }

        private Expression<Func<buy_Offer, bool>> CreateFilter()
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

            Expression<Func<buy_Offer, bool>> filter = b => b.buyer_id != _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }

        public async Task Accept()
        {
            product sellerProduct = null;
            var productQuery = await _productService.GetData(prod => prod.product_owner == _user.Id && prod.Name == SelectedOffer.Product.Name);
            if(productQuery.Count() != 0)
            {
                sellerProduct = productQuery.First();
            }
            if(sellerProduct == null)
            {
                return;
            }
            if(sellerProduct.stock < SelectedOffer.Amount)
            {
                return;
            }

            SelectedOffer.StatusId = (int)TransactionState.Finished;
            var sellOffer = new sell_Offer()
            {
                name = SelectedOffer.Name,
                Update_Who = _user.Id,
                Update_Date = DateTime.Now,
                seller_id = _user.Id,
                status_id = (int)TransactionState.Finished,
                product_id = sellerProduct.ID,
                price = (double)SelectedOffer.Price,
                amount = SelectedOffer.Amount,
            };
            SelectedOffer.NullNavigationProperties();
            SelectedOffer.Product.NullNavigationProperties();
            bool result = await _transactionService.AcceptBuyTransaction(sellOffer, SelectedOffer.BuyOffer,
                SelectedOffer.Product.Product, sellerProduct);

            Offers.Remove(SelectedOffer);
        }
    }
}
