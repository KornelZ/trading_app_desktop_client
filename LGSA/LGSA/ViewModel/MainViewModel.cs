using LGSA.Model.UnitOfWork;
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
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private AsyncRelayCommand _buyOfferVMCommand;
        private AsyncRelayCommand _searchCommand;
        private object _displayedView;
        private FilterViewModel _filter;

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
        public MainViewModel()
        {
            _unitOfWorkFactory = new DbUnitOfWorkFactory();
            _authenticationVM = new AuthenticationViewModel(_unitOfWorkFactory);
            _authenticationVM.Authentication += GoToProductVM;
            _filter = new FilterViewModel();
            BuyOfferVMCommand = new AsyncRelayCommand(execute => GoToBuyOfferVM(), canExecute => { return true; });
            SearchCommand = new AsyncRelayCommand(execute => Search(), canExecute => { return true; });
            DisplayedView = _authenticationVM;
        }

        private async Task GoToProductVM(object sender, EventArgs e)
        {
            DictionaryVM = new DictionaryViewModel(_unitOfWorkFactory);
            await DictionaryVM.LoadDictionaries();
            _productVM = new ProductViewModel(_unitOfWorkFactory, Filter, _authenticationVM.User.User);
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
