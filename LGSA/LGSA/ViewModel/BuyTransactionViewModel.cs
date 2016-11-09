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
    public class BuyTransactionViewModel : BindableBase
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
        public async Task LoadOffers()
        {
            double price = (double)_filter.ParsedPrice();
            double rating = _filter.ParsedRating();
            int stock = _filter.ParsedStock();
            //Expression<Func<buy_Offer, bool>> predicate = b => b.buyer_id != _user.Id && b.product.Name.Contains(_filter.Name)
            //    && b.price <= price && b.product.rating >= rating
            //    && b.product.stock >= stock && b.product.dic_Genre.name.Contains(_filter.Genre.Name)
            //    && b.product.dic_condition.name.Contains(_filter.Condition.Name);
            Expression<Func<buy_Offer, bool>> predicate = b => b.buyer_id != _user.Id && b.status_id != 3;
            var offers = await _buyOfferService.GetData(predicate);
            Offers.Clear();

            foreach(var offer in offers)
            {
                Offers.Add(BuyOfferWrapper.CreateBuyOffer(offer));
            }
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

            SelectedOffer.StatusId = 3;
            var sellOffer = new sell_Offer()
            {
                name = SelectedOffer.Name,
                Update_Who = _user.Id,
                Update_Date = DateTime.Now,
                seller_id = _user.Id,
                status_id = 3,
                product_id = sellerProduct.ID,
                price = (double)SelectedOffer.Price,
                amount = SelectedOffer.Amount,
            };
            SelectedOffer.NullNavigationProperties();
            SelectedOffer.Product.NullNavigationProperties();
            bool result = await _transactionService.AcceptBuyTransaction(sellOffer, SelectedOffer.BuyOffer,
                SelectedOffer.Product.Product, sellerProduct);
        }
    }
}
