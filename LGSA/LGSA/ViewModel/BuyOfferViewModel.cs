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

        private string _errorString;
        public BuyOfferViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _user = user;
            _buyOfferService = new BuyOfferService(factory);
            BuyOffers = new BindableCollection<BuyOfferWrapper>();
            CreatedOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);

            _filter = filter;

            UpdateCommand = new AsyncRelayCommand(execute => UpdateOffer(), canExecute => CanModifyOffer());
            DeleteCommand = new AsyncRelayCommand(execute => DeleteOffer(), canExecute => CanModifyOffer());
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
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; Notify(); }
        }
        public async Task Load()
        {
            Expression<Func<buy_Offer, bool>> filter = CreateFilter();
            var offers = await _buyOfferService.GetData(filter);
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

            Expression<Func<buy_Offer, bool>> filter = b => b.buyer_id == _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }
        public async Task AddOffer()
        {
            if(_createdOffer.Name == null || _createdOffer.Name == "" || _createdOffer.Product.Name == null || CreatedOffer.Amount <= 0 || CreatedOffer?.Price <= 0)
            {
                ErrorString = (string)Application.Current.FindResource("InvalidBuyOfferError");
                return;
            }
            CreatedOffer.Product.CheckForNull();
            bool offerAdded = await _buyOfferService.Add(_createdOffer.BuyOffer);

            if(offerAdded == true)
            {
                BuyOffers.Add(_createdOffer);
                _createdOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);
            }
            else
            {
                ErrorString = (string)Application.Current.FindResource("InsertBuyOfferError");
                return;
            }
            ErrorString = null;
        }
        public async Task UpdateOffer()
        {
            bool offerUpdated = await _buyOfferService.Update(_selectedOffer.BuyOffer);
            if(offerUpdated == false)
            {
                ErrorString = (string)Application.Current.FindResource("UpdateBuyOfferError");
            }
            ErrorString = null;
        }
        public async Task DeleteOffer()
        {
            bool offerDeleted = await _buyOfferService.Delete(_selectedOffer.BuyOffer);
            if(offerDeleted == true)
            {
                BuyOffers.Remove(_selectedOffer);
            }
            else
            {
                ErrorString = (string)Application.Current.FindResource("DeleteBuyOfferError");
                return;
            }
            ErrorString = null;
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
