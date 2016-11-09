﻿using LGSA.Model.UnitOfWork;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.ViewModel
{
    public class MainViewModel : Utility.BindableBase
    {
        private DictionaryViewModel _dictionaryVM;
        private AuthenticationViewModel _authenticationVM;
        private ProductViewModel _productVM;
        private BuyOfferViewModel _buyOfferVM;
        private SellOfferViewModel _sellOfferVM;
        private BuyTransactionViewModel _buyTransactionVM;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        private AsyncRelayCommand _buyOfferVMCommand;
        private AsyncRelayCommand _buyTransactionVMCommand;
        private AsyncRelayCommand _sellOfferVMCommand;
        private AsyncRelayCommand _searchCommand;
        private AsyncRelayCommand _productVMCommand;
        private object _displayedView;
        private FilterViewModel _filter;


        public AsyncRelayCommand ProductVMCommand {
            get { return _productVMCommand; }
            set { _productVMCommand = value; Notify(); }
        }
        public FilterViewModel Filter
        {
            get { return _filter; }
            set { _filter = value; Notify(); }
        }

        public DictionaryViewModel DictionaryVM
        {
            get { return _dictionaryVM; }
            set { _dictionaryVM = value; Notify(); }
        }
        public object DisplayedView
        {
            get { return _displayedView; }
            set { _displayedView = value; Notify(); }
        }

        public AsyncRelayCommand SearchCommand
        {
            get { return _searchCommand; }
            set { _searchCommand = value; Notify(); }
        }
        public AsyncRelayCommand BuyOfferVMCommand
        {
            get { return _buyOfferVMCommand; }
            set { _buyOfferVMCommand = value; Notify(); }
        }
        public AsyncRelayCommand SellOfferVMCommand
        {
            get { return _sellOfferVMCommand; }
            set { _sellOfferVMCommand = value; Notify(); }
        }
        public AsyncRelayCommand BuyTransactionVMCommand
        {
            get { return _buyTransactionVMCommand; }
            set { _buyTransactionVMCommand = value; Notify(); }
        }
        public MainViewModel()
        {
            _unitOfWorkFactory = new DbUnitOfWorkFactory();
            _authenticationVM = new AuthenticationViewModel(_unitOfWorkFactory);
            _authenticationVM.Authentication += GoToProductVM;
            _filter = new FilterViewModel();
            BuyOfferVMCommand = new AsyncRelayCommand(execute => GoToBuyOfferVM(), canExecute => { return true; });
            SellOfferVMCommand = new AsyncRelayCommand(execute => GoToSellOfferVM(), canExecute => { return true; });
            ProductVMCommand = new AsyncRelayCommand(execute => GoToProductVM(null, null), canExecute => { return true; });
            SearchCommand = new AsyncRelayCommand(execute => Search(), canExecute => { return true; });
            BuyTransactionVMCommand = new AsyncRelayCommand(execute => GoToBuyTransactionVM(), _canExecute => { return true; });
            DisplayedView = _authenticationVM;
        }

        private async Task GoToProductVM(object sender, EventArgs e)
        {
            if(_dictionaryVM == null)
            {
                DictionaryVM = new DictionaryViewModel(_unitOfWorkFactory);
                await DictionaryVM.LoadDictionaries();
            }
            if(_productVM == null)
            {
                _productVM = new ProductViewModel(_unitOfWorkFactory, Filter, _authenticationVM.User.User);
            }
            
            await _productVM.GetProducts();
            DisplayedView = _productVM;
            /* do dokończenia */
        }

        private async Task GoToBuyOfferVM()
        {
            if(_buyOfferVM == null)
            {
                _buyOfferVM = new BuyOfferViewModel(_unitOfWorkFactory, _authenticationVM.User.User);
            }
            await _buyOfferVM.LoadOffers();
            DisplayedView = _buyOfferVM;
        }

        private async Task GoToSellOfferVM()
        {
            if (_sellOfferVM == null)
            {
                _sellOfferVM = new SellOfferViewModel(_unitOfWorkFactory, _authenticationVM.User.User, _productVM.Products);
            }
            await _sellOfferVM.LoadOffers();
            DisplayedView = _sellOfferVM;
        }

        private async Task GoToBuyTransactionVM()
        {
            if(_buyTransactionVM == null)
            {
                _buyTransactionVM = new BuyTransactionViewModel(_unitOfWorkFactory, Filter, _authenticationVM.User.User);
            }
            await _buyTransactionVM.LoadOffers();
            DisplayedView = _buyTransactionVM;
        }

        private async Task Search()
        {
            //trzeba switcha
            if (_productVM != null)
            {
                await _productVM.GetProducts();
            }

        }
    }
}
