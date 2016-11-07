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
        private AuthenticationViewModel _authenticationVM;
        private ProductViewModel _productVM;
        private BuyOfferViewModel _buyOfferVM;
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private AsyncRelayCommand _buyOfferVMCommand;
        private object _displayedView;

        public object DisplayedView
        {
            get { return _displayedView; }
            set { _displayedView = value; Notify(); }
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
            BuyOfferVMCommand = new AsyncRelayCommand(execute => GoToBuyOfferVM(), canExecute => { return true; });
            DisplayedView = _authenticationVM;
        }

        private async Task GoToProductVM(object sender, EventArgs e)
        {
            _productVM = new ProductViewModel(new DbUnitOfWorkFactory());
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
    }
}
