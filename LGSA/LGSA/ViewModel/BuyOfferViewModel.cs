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
    public class BuyOfferViewModel : BindableBase
    {
        private UserWrapper _user;
        private BuyOfferService _buyOfferService;
        private BindableCollection<BuyOfferWrapper> _buyOffers;
        private BuyOfferWrapper _createdOffer;
        private BuyOfferWrapper _selectedOffer;

        private AsyncRelayCommand _updateCommand;
        private AsyncRelayCommand _deleteCommand;
        public BuyOfferViewModel(IUnitOfWorkFactory factory, UserWrapper user)
        {
            _user = user;
            _buyOfferService = new BuyOfferService(factory);
            BuyOffers = new BindableCollection<BuyOfferWrapper>();
            CreatedOffer = BuyOfferWrapper.CreateBuyOffer(_user);

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
        public async Task LoadOffers()
        {
            Expression<Func<buy_Offer, bool>> filter = b => b.buyer_id == _user.Id;
            var offers = await _buyOfferService.GetData(filter);

            foreach(var o in offers)
            {
                var product = new ProductWrapper(o.product);
                product.Genre = new GenreWrapper(o.product.dic_Genre);
                product.Condition = new ConditionWrapper(o.product.dic_condition);
                product.ProductType = new ProductTypeWrapper(o.product.dic_Product_type);
                BuyOffers.Add(new BuyOfferWrapper(o)
                {
                    Product = product,
                });
            }
        }
        public async Task AddOffer()
        {
            bool offerAdded = await _buyOfferService.Add(_createdOffer.BuyOffer);

            if(offerAdded == true)
            {
                BuyOffers.Add(_createdOffer);
                _createdOffer = BuyOfferWrapper.CreateBuyOffer(_user);
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
