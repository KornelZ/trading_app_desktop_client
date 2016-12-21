using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using System.Windows.Input;
using System.Linq.Expressions;
using LGSA.Model;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using LGSA_Server.Model.DTO;
using System.Windows.Controls;

namespace LGSA.ViewModel
{
    public class AuthenticationViewModel : Utility.BindableBase, IViewModel
    {
        public delegate Task AuthenticationEventHandler(object sender, EventArgs e);
        public event AuthenticationEventHandler Authentication;
        private UserAuthenticationWrapper _user;
        private AsyncRelayCommand _registerCommand;
        private AsyncRelayCommand _authenticateCommand;
        private AsyncRelayCommand _submitCommand;
        private AsyncRelayCommand _cancelCommand;
        private Visibility _loginChoice;
        private Visibility _registerChoice;
        private string _errorString;

        public Visibility LoginChoice
        {
            get { return _loginChoice; }
            set { _loginChoice = value; Notify(); }
        }
        public Visibility RegisterChoice
        {
            get { return _registerChoice; }
            set { _registerChoice = value; Notify(); }
        }
        public AsyncRelayCommand CancelCommand{ get { return _cancelCommand; } set { _cancelCommand = value; Notify(); } }
        public AsyncRelayCommand SubmitCommand
        {
            get { return _submitCommand; }
            set { _submitCommand = value; Notify(); }
        }
        public AsyncRelayCommand RegisterCommand
        {
            get { return _registerCommand; }
            set { _registerCommand = value; Notify(); }
        }
        public AsyncRelayCommand AuthenticateCommand
        {
            get { return _authenticateCommand; }
            set { _authenticateCommand = value; Notify(); }
        }
        public UserAuthenticationWrapper User
        {
            get { return _user; }
            set { _user = value; Notify(); }
        }

        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; Notify(); }
        }

        public AuthenticationViewModel()
        {
            _user = new UserAuthenticationWrapper(new Model.users_Authetication() { Update_Date = DateTime.Now, Update_Who = 1 });
            _user.User = new UserWrapper(new Model.users() { Update_Date = DateTime.Now, Update_Who = 1});
            _user.User.Address = new AddressDto();
            RegisterCommand = new AsyncRelayCommand(execute => Register(), canExecute => true);
            CancelCommand = new AsyncRelayCommand(execute => Cancel(), canExecute => true);
            AuthenticateCommand = new AsyncRelayCommand(execute => Authenticate(), canExecute => CanAuthenticate());
            SubmitCommand = new AsyncRelayCommand(execute => Submit(), canExecute => CanSubmit());
            LoginChoice = Visibility.Visible;
            RegisterChoice = Visibility.Collapsed;
        }

        protected virtual async Task OnAuthentication(EventArgs e)
        {
            await Authentication?.Invoke(this, e);
        }

        public async Task Register()
        {
            LoginChoice = Visibility.Collapsed;
            RegisterChoice = Visibility.Visible;
        }
        public async Task Cancel()
        {
            LoginChoice = Visibility.Visible;
            RegisterChoice = Visibility.Collapsed;
        }
        public bool CanAuthenticate()
        {
            if(_user.Password == null || _user.User.Login == null || !AuthenticateCommand.IsAvailable)
            {
                return false;
            }
            return true;
        }
        public async Task Submit()
        {
            LoginChoice = Visibility.Visible;
            RegisterChoice = Visibility.Collapsed;
            using (var client = new HttpClient())
            {
                AuthenticationDto content = createAuthencticationDto();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                URLBuilder url = new URLBuilder("/api/User/");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")

                };
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorString = (string)Application.Current.FindResource("RegistrationError");
                    return;
                }
                var contents = await response.Content.ReadAsStringAsync();
                AuthenticationDto result = JsonConvert.DeserializeObject<AuthenticationDto>(contents);
                User.UserId = result.UserId;
            }
        }
        public bool CanSubmit()
        {
            if (_user.Password == null || _user.User.LastName == null || _user.User.FirstName == null || _user.User.Login == null ||
                _user.User.Address.City == null || _user.User.Address.PostalCode == null || _user.User.Address.Street == null)
            {
                return false;
            }
            return true;
        }
        public async Task Authenticate()
        {
            using (var client = new HttpClient())
            {
                AuthenticationDto content = createAuthencticationDto();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                URLBuilder url = new URLBuilder("/Login/");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")
                };
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                if(!response.IsSuccessStatusCode)
                {
                    ErrorString = (string)Application.Current.FindResource("AuthenticationError");
                    return;
                }
                AuthenticationDto result = JsonConvert.DeserializeObject<AuthenticationDto>(contents);
                User.UserId =  result.User.Id;
                User.Id = result.Id;
                User.User.FirstName = result.User.FirstName;
                User.User.LastName = result.User.LastName;
                User.User.Address = new AddressDto();
                User.User.Address.Id = result.User.Address.Id;
                User.User.Address.City = result.User.Address.City;
                User.User.Address.Street = result.User.Address.Street;
                User.User.Address.PostalCode = result.User.Address.PostalCode;
            }
            ErrorString = null;
            await OnAuthentication(EventArgs.Empty);
        }

        public Task Load()
        {
            throw new NotImplementedException();
        }

        AuthenticationDto createAuthencticationDto()
        {
            AuthenticationDto content = new AuthenticationDto();
            content.Id = 0;
            content.UserId = 0;
            content.Password = User.Password;
            content.User = new UserDto();
            content.User.UserName = "0";
            content.User.FirstName = "0";
            content.User.AddressId = 0;
            content.User.LastName = "0";
            content.User.Rating = 0;
            content.User.Id = 0;
            content.User.Address = new AddressDto();
            content.User.Address.City = "0";
            content.User.Address.PostalCode = "0";
            content.User.Address.Street = "0";
            content.User.Address.Id = 0;
            if (User.User.FirstName != null) { content.User.FirstName = User.User.FirstName; }
            if (User.User.LastName != null) { content.User.LastName = User.User.LastName; }
            if (User.User.Login != null) { content.User.UserName = User.User.Login; }
            if (User.User.Address.City != null) { content.User.Address.City = User.User.Address.City; }
            if (User.User.Address.Street != null) { content.User.Address.Street = User.User.Address.Street; }
            if (User.User.Address.PostalCode != null) { content.User.Address.PostalCode = User.User.Address.PostalCode; }

            return content;
        }
    }
}
