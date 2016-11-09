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
    public class SellTransactionViewModel : BindableBase
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
        public async Task LoadOffers()
        {
            double price = (double)_filter.ParsedPrice();
            double rating = _filter.ParsedRating();
            int stock = _filter.ParsedStock();
            //Expression<Func<buy_Offer, bool>> predicate = b => b.buyer_id != _user.Id && b.product.Name.Contains(_filter.Name)
            //    && b.price <= price && b.product.rating >= rating
            //    && b.product.stock >= stock && b.product.dic_Genre.name.Contains(_filter.Genre.Name)
            //    && b.product.dic_condition.name.Contains(_filter.Condition.Name);
            Expression<Func<sell_Offer, bool>> predicate = b => b.seller_id != _user.Id && b.status_id != 3;
            var offers = await _sellOfferService.GetData(predicate);
            Offers.Clear();

            foreach (var offer in offers)
            {
                Offers.Add(SellOfferWrapper.CreateSellOffer(offer));
            }
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
