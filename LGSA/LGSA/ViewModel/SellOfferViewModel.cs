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
    public class SellOfferViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private SellOfferService _sellOfferService;
        private ProductService _productService;
        private BindableCollection<SellOfferWrapper> _sellOffers;
        private BindableCollection<ProductWrapper> _products;
        private SellOfferWrapper _createdOffer;
        private SellOfferWrapper _selectedOffer;

        private FilterViewModel _filter;
        private AsyncRelayCommand _updateCommand;
        private AsyncRelayCommand _deleteCommand;
        public SellOfferViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _user = user;
            _sellOfferService = new SellOfferService(factory);
            _productService = new ProductService(factory);

            _filter = filter;

            _products = new BindableCollection<ProductWrapper>();
            SellOffers = new BindableCollection<SellOfferWrapper>();
            CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);

            UpdateCommand = new AsyncRelayCommand(execute => UpdateOffer(), canExecute => CanModifyOffer());
            DeleteCommand = new AsyncRelayCommand(execute => DeleteOffer(), canExecute => CanModifyOffer());
        }

        public BindableCollection<SellOfferWrapper> SellOffers
        {
            get { return _sellOffers; }
            set { _sellOffers = value; Notify(); }
        }
        public BindableCollection<ProductWrapper> Products
        {
            get { return _products; }
            set { _products = value; Notify(); }
        }
        public SellOfferWrapper CreatedOffer
        {
            get { return _createdOffer; }
            set { _createdOffer = value; Notify(); }
        }
        public SellOfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value; Notify(); }
        }
        public AsyncRelayCommand UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = value; Notify(); }
        }
        public AsyncRelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; Notify(); }
        }
        public async Task Load()
        {
            var offers = await _sellOfferService.GetData(CreateFilter());
            SellOffers.Clear();
            foreach (var o in offers)
            {
                var product = ProductWrapper.CreateProduct(o.product);
                SellOffers.Add(new SellOfferWrapper(o)
                {
                    Product = product,
                });
            }

            await RefreshProducts();
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

            Expression<Func<sell_Offer, bool>> filter = b => b.seller_id == _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }

        private async Task RefreshProducts()
        {
            Expression<Func<product, bool>> filter = p => p.product_owner == _user.Id && p.stock > 0;
            var products = await _productService.GetData(filter);
            Products.Clear();
            foreach(var p in products)
            {
                var product = ProductWrapper.CreateProduct(p);
                Products.Add(product);
            }
        }
        public async Task AddOffer()
        {
            if(CreatedOffer.Name == null || CreatedOffer.Product == null || CreatedOffer.Product.Id == 0 
                || CreatedOffer.Amount <= 0 || CreatedOffer.Amount > CreatedOffer.Product.Stock || CreatedOffer?.Price <= 0)
            {
                CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);
                return;
            }
            // if all offers' product amount is greater than product stock, then fail
            var productOffers = await _sellOfferService.GetData(offer => offer.seller_id == _user.Id && offer.product_id == CreatedOffer.Product.Id);
            var totalAmount = productOffers.Sum(offer => offer.amount);
            if(CreatedOffer.Amount + totalAmount > CreatedOffer.Product.Stock)
            {
                CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);
                return;
            }
            bool offerAdded = await _sellOfferService.Add(_createdOffer.SellOffer);

            if (offerAdded == true)
            {
                SellOffers.Add(_createdOffer);
                await Load();
                _createdOffer = SellOfferWrapper.CreateSellOffer(_user);
            }
        }
        public async Task UpdateOffer()
        {
            bool offerUpdated = await _sellOfferService.Update(_selectedOffer.SellOffer);
        }
        public async Task DeleteOffer()
        {
            bool offerDeleted = await _sellOfferService.Delete(_selectedOffer.SellOffer);
            if (offerDeleted == true)
            {
                SellOffers.Remove(_selectedOffer);
            }
        }

        public bool CanModifyOffer()
        {
            if (_selectedOffer == null)
            {
                return false;
            }
            return true;
        }
    }
}
