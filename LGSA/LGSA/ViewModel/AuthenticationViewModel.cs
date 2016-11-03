using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LGSA.Model.Services;
using LGSA.Model.UnitOfWork;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using System.Windows.Input;

namespace LGSA.ViewModel
{
    public class AuthenticationViewModel : Utility.BindableBase
    {
        private AuthenticationService _authenticationService;
        private UserAuthenticationWrapper _user;
        private bool _registered;
        private AsyncRelayCommand _registerCommand;
        public AsyncRelayCommand RegisterCommand
        {
            get { return _registerCommand; }
            set { _registerCommand = value; Notify(); }
        }
        public UserAuthenticationWrapper User
        {
            get { return _user; }
            set { _user = value; Notify(); }
        }
        public bool Registered
        {
            get { return _registered; }
            set { _registered = value; Notify(); }
        }
        public AuthenticationViewModel(IUnitOfWorkFactory factory)
        {
            _authenticationService = new AuthenticationService(factory);
            _user = new UserAuthenticationWrapper(new Model.users_Authetication());
            _user.User = new UserWrapper(new Model.users());

            _registerCommand = new AsyncRelayCommand(execute => Register(), canExecute => CanRegister());
        }

        public async Task Register()
        {
            Registered = await _authenticationService.Add(_user.UserAuthentication);
        }
        public bool CanRegister()
        {
            if(_user.Password == null || _user.User.FirstName == null 
                || _user.User.LastName == null)
            {
                return false;
            }
            return true;
        }
        public async Task Authenticate()
        {

        }
    }
}
