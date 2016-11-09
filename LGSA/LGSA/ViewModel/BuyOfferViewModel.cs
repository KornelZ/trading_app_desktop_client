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
    public class BuyOfferViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private BuyOfferService _buyOfferService;

        private BindableCollection<BuyOfferWrapper> _buyOffers;

        private BuyOfferWrapper _createdOffer;
        private BuyOfferWrapper _selectedOffer;

        private AsyncRelayCommand _updateCommand;
        private AsyncRelayCommand _deleteCommand;

        private FilterViewModel _filter;
        public BuyOfferViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _user = user;
            _buyOfferService = new BuyOfferService(factory);
            BuyOffers = new BindableCollection<BuyOfferWrapper>();
            CreatedOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);

            UpdateCommand = new AsyncRelayCommand(execute => UpdateOffer(), canExecute => CanModifyOffer());
            DeleteCommand = new AsyncRelayCommand(execute => DeleteOffer(), canExecute => CanModifyOffer());

            _filter = filter;
        }

        public BindableCollection<BuyOfferWrapper> BuyOffers
        {
            get { return _buyOffers; }
            set { _buyOffers = value; Notify(); }
        }
        public BuyOfferWrapper CreatedOffer
        {
            get { return _createdOffer; }
            set { _createdOffer = value; Notify(); }
        }
        public BuyOfferWrapper SelectedOffer
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
            double price = (double)_filter.ParsedPrice();
            double rating = _filter.ParsedRating();
            int stock = _filter.ParsedStock();
            string condition = "";
            string genre = "";
            if (!_filter.Condition.Name.Equals("All/Any"))
            {
                condition = _filter.Condition.Name;
            }
            if (!_filter.Genre.Name.Equals("All/Any"))
            {
                genre = _filter.Genre.Name;
            }
            Expression<Func<buy_Offer, bool>> predicate = b => b.buyer_id != _user.Id && b.status_id != 3 && b.product.Name.Contains(_filter.Name)
               && b.price <= price && b.product.rating >= rating
               && b.product.stock >= stock && b.product.dic_Genre.name == genre
               && b.product.dic_condition.name == condition;

            var offers = await _buyOfferService.GetData(predicate);
            BuyOffers.Clear();
            foreach(var o in offers)
            {
                var product = ProductWrapper.CreateProduct(o.product);
                BuyOffers.Add(new BuyOfferWrapper(o)
                {
                    Product = product,
                });
            }
        }
        public async Task AddOffer()
        {
            if(_createdOffer.Name == null || _createdOffer.Product.Name == null || CreatedOffer.Amount <= 0 || CreatedOffer?.Price <= 0)
            {
                return;
            }
            CreatedOffer.Product.CheckForNull();
            bool offerAdded = await _buyOfferService.Add(_createdOffer.BuyOffer);

            if(offerAdded == true)
            {
                BuyOffers.Add(_createdOffer);
                _createdOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);
            }
        }
        public async Task UpdateOffer()
        {
            bool offerUpdated = await _buyOfferService.Update(_selectedOffer.BuyOffer);
        }
        public async Task DeleteOffer()
        {
            bool offerDeleted = await _buyOfferService.Delete(_selectedOffer.BuyOffer);
            if(offerDeleted == true)
            {
                BuyOffers.Remove(_selectedOffer);
            }
        }

        public bool CanModifyOffer()
        {
            if(_selectedOffer == null)
            {
                return false;
            }
            return true;
        }
    }
}
