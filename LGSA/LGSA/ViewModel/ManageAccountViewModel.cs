using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using LGSA_Server.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LGSA.ViewModel
{
    public sealed class ManageAccountViewModel : BindableBase, IViewModel
    {
        UserAuthenticationWrapper _authUser;
        private AsyncRelayCommand _submit;
        private string _errorString;
        private string _realSecure;

        public AsyncRelayCommand Submit { get { return _submit; } set { _submit = value; Notify(); } }
        public UserAuthenticationWrapper User{ get { return _authUser; } set { _authUser = value; Notify(); } }
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; Notify(); }
        }
        public ManageAccountViewModel(UserAuthenticationWrapper authUser)
        {
            _authUser = authUser;
            _realSecure = authUser.Password;
            Submit = new AsyncRelayCommand(execute => SubmitCommand(), canExecute => { return true; });
        }
        public async Task SubmitCommand()
        {
            using (var client = new HttpClient())
            {


                AuthenticationDto content = createAuthencticationDto();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                URLBuilder url = new URLBuilder("/api/User/");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Put,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")

                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", User.UserId.ToString(), _realSecure.ToString()))));
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorString = (string)System.Windows.Application.Current.FindResource("RegistrationError");
                    return;
                }else
                {
                    ErrorString = (string)System.Windows.Application.Current.FindResource("SuccessfullAccountUpdate");
                }
                var contents = await response.Content.ReadAsStringAsync();
                AuthenticationDto result = JsonConvert.DeserializeObject<AuthenticationDto>(contents);
            }
        }
        AuthenticationDto createAuthencticationDto()
        {
            AuthenticationDto content = new AuthenticationDto();
            content.Id = User.Id;
            content.UserId = User.UserId;
            content.Password = User.Password;
            content.User = new UserDto();
            content.User.UserName = "0";
            content.User.FirstName = "0";
            content.User.AddressId = User.User.Address.Id;
            content.User.LastName = "0";
            content.User.Rating = 0;
            content.User.Id = User.UserId;
            content.User.Address = new AddressDto();
            content.User.Address.City = "0";
            content.User.Address.PostalCode = "0";
            content.User.Address.Street = "0";
            content.User.Address.Id = User.User.Address.Id;
            if (User.User.FirstName != null) { content.User.FirstName = User.User.FirstName; }
            if (User.User.LastName != null) { content.User.LastName = User.User.LastName; }
            if (User.User.Login != null) { content.User.UserName = User.User.Login; }
            if (User.User.Address.City != null) { content.User.Address.City = User.User.Address.City; }
            if (User.User.Address.Street != null) { content.User.Address.Street = User.User.Address.Street; }
            if (User.User.Address.PostalCode != null) { content.User.Address.PostalCode = User.User.Address.PostalCode; }

            return content;
        }
        public async Task Load()
        {

            await Task.FromResult(0);
        }
    }
}
