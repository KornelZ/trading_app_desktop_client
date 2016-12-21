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
        private SellTransactionViewModel _sellTransactionVM;
        private ManageAccountViewModel _manageAccountVm;

        private AsyncRelayCommand _buyOfferVMCommand;
        private AsyncRelayCommand _buyTransactionVMCommand;
        private AsyncRelayCommand _sellOfferVMCommand;
        private AsyncRelayCommand _sellTransactionVMCommand;
        private AsyncRelayCommand _searchCommand;
        private AsyncRelayCommand _productVMCommand;
        private AsyncRelayCommand _manageAccountVMCommand;

        private RelayCommand _logoutCommand;

        private IViewModel _displayedView;
        private FilterViewModel _filter;
        private bool _isUserAuthenticated;

        public AsyncRelayCommand ProductVMCommand {
            get { return _productVMCommand; }
            set { _productVMCommand = value; Notify(); }
        }
        public AsyncRelayCommand ManageAccountVMCommand { get { return _manageAccountVMCommand; } set { _manageAccountVMCommand = value; Notify(); } }
        public FilterViewModel Filter
        {
            get { return _filter; }
            set { _filter = value; Notify(); }
        }
        public ManageAccountViewModel ManageAccountVM { get { return _manageAccountVm; } set { _manageAccountVm = value; Notify(); } }
        public DictionaryViewModel DictionaryVM
        {
            get { return _dictionaryVM; }
            set { _dictionaryVM = value; Notify(); }
        }
        public IViewModel DisplayedView
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
        public AsyncRelayCommand SellTransactionVMCommand
        {
            get { return _sellTransactionVMCommand; }
            set { _sellTransactionVMCommand = value; Notify(); }
        }
        public RelayCommand LogoutCommand
        {
            get { return _logoutCommand; }
            set { _logoutCommand = value; Notify(); }
        }

        public bool IsUserAuthenticated
        {
            get { return _isUserAuthenticated; }
            set { _isUserAuthenticated = value; Notify(); }
        }

        public MainViewModel()
        {
            _authenticationVM = new AuthenticationViewModel();
            _authenticationVM.Authentication += GoToProductVM;
            _filter = new FilterViewModel();

            BuyOfferVMCommand = new AsyncRelayCommand(execute => GoToBuyOfferVM(), canExecute => { return true; });
            SellOfferVMCommand = new AsyncRelayCommand(execute => GoToSellOfferVM(), canExecute => { return true; });
            ProductVMCommand = new AsyncRelayCommand(execute => GoToProductVM(null, null), canExecute => { return true; });
            SearchCommand = new AsyncRelayCommand(execute => Search(), canExecute => { return true; });
            BuyTransactionVMCommand = new AsyncRelayCommand(execute => GoToBuyTransactionVM(), canExecute => { return true; });
            SellTransactionVMCommand = new AsyncRelayCommand(execute => GoToSellTransactionVM(), canExecute => { return true; });
            ManageAccountVMCommand = new AsyncRelayCommand(execute => GoToManageAccountVM(), canExecute => { return true; });

            LogoutCommand = new RelayCommand(execute => Logout(), canExecute => { return true; });

            IsUserAuthenticated = false;
            DisplayedView = _authenticationVM;
        }

        private async Task GoToProductVM(object sender, EventArgs e)
        {
            if(_dictionaryVM == null)
            {
                DictionaryVM = new DictionaryViewModel();
                await DictionaryVM.Load();
            }
            if(_productVM == null)
            {
                _productVM = new ProductViewModel(Filter, _authenticationVM.User.User, DictionaryVM, _authenticationVM.User);
            }
            
            await _productVM.Load();
            IsUserAuthenticated = true;
            DisplayedView = _productVM;
            /* do dokończenia */
        }

        private async Task GoToBuyOfferVM()
        {
            if(_buyOfferVM == null)
            {
                _buyOfferVM = new BuyOfferViewModel(Filter, _authenticationVM.User.User, _authenticationVM.User);
            }
            await _buyOfferVM.Load();
            DisplayedView = _buyOfferVM;
        }
        private async Task GoToManageAccountVM()
        {
            if(_manageAccountVm == null)
            {
                _manageAccountVm = new ManageAccountViewModel(_authenticationVM.User);
            }
            DisplayedView = _manageAccountVm;
        }

        private async Task GoToSellOfferVM()
        {
            if (_sellOfferVM == null)
            {
                _sellOfferVM = new SellOfferViewModel(Filter, _authenticationVM.User.User, _authenticationVM.User);
            }
            await _sellOfferVM.Load();
            DisplayedView = _sellOfferVM;
        }

        private async Task GoToBuyTransactionVM()
        {
            if(_buyTransactionVM == null)
            {
                _buyTransactionVM = new BuyTransactionViewModel(Filter, _authenticationVM.User.User, _authenticationVM.User);
            }
            await _buyTransactionVM.Load();
            DisplayedView = _buyTransactionVM;
        }

        private async Task GoToSellTransactionVM()
        {
            if(_sellTransactionVM == null)
            {
                _sellTransactionVM = new SellTransactionViewModel(Filter, _authenticationVM.User.User, _authenticationVM.User);
            }
            await _sellTransactionVM.Load();
            DisplayedView = _sellTransactionVM;
        }

        private void Logout()
        {
            _productVM = null;
            _buyOfferVM = null;
            _sellOfferVM = null;
            _buyTransactionVM = null;
            _sellTransactionVM = null;

            _authenticationVM.User.Password = null;
            IsUserAuthenticated = false;
            DisplayedView = _authenticationVM;
        }

        private async Task Search()
        {
            await DisplayedView.Load();
        }
    }
}
