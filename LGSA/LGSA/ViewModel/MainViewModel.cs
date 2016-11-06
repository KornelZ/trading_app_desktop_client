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
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private object _displayedView;

        public object DisplayedView
        {
            get { return _displayedView; }
            set { _displayedView = value; Notify(); }
        }

        public MainViewModel()
        {
            _unitOfWorkFactory = new DbUnitOfWorkFactory();
            _authenticationVM = new AuthenticationViewModel(_unitOfWorkFactory);
            _authenticationVM.Authentication += GoToProductVM;
            DisplayedView = _authenticationVM;
        }

        private async Task GoToProductVM(object sender, EventArgs e)
        {
            _productVM = new ProductViewModel();
            DisplayedView = _productVM;
            /* do dokończenia */
        }
    }
}
