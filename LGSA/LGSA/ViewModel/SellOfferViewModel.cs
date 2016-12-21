using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using LGSA_Server.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LGSA.ViewModel
{
    public class SellOfferViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private UserAuthenticationWrapper _authenticationUser;
        private BindableCollection<SellOfferWrapper> _sellOffers;
        private BindableCollection<ProductWrapper> _products;
        private SellOfferWrapper _createdOffer;
        private SellOfferWrapper _selectedOffer;
        private readonly string controler = "/api//SellOffer/";
        private FilterViewModel _filter;
        private AsyncRelayCommand _updateCommand;
        private AsyncRelayCommand _deleteCommand;

        private string _errorString;
        public SellOfferViewModel(FilterViewModel filter, UserWrapper user, UserAuthenticationWrapper authenticationUser)
        {
            _authenticationUser = authenticationUser;
            _user = user;
            _filter = filter;
            _products = new BindableCollection<ProductWrapper>();
            SellOffers = new BindableCollection<SellOfferWrapper>();
            CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);
            UpdateCommand = new AsyncRelayCommand(execute => UpdateOffer(), canExecute => CanModifyOffer());
            DeleteCommand = new AsyncRelayCommand(execute => DeleteOffer(), canExecute => CanModifyOffer());
        }

        public BindableCollection<SellOfferWrapper> SellOffers
        {
            get { return _sellOffers; }
            set { _sellOffers = value; Notify(); }
        }
        public BindableCollection<ProductWrapper> Products
        {
            get { return _products; }
            set { _products = value; Notify(); }
        }
        public SellOfferWrapper CreatedOffer
        {
            get { return _createdOffer; }
            set { _createdOffer = value; Notify(); }
        }
        public SellOfferWrapper SelectedOffer
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
            using (var client = new HttpClient())
            {
                URLBuilder url = new URLBuilder(_filter, controler);
                url.URL += "&ShowMyOffers=true";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<SellOfferDto> result = JsonConvert.DeserializeObject<List<SellOfferDto>>(contents);
                SellOffers.Clear();
                foreach (SellOfferDto bo in result)
                {
                    SellOfferWrapper boffer = bo.createSellOffer();
                    SellOffers.Add(boffer);
                }
            }
            await RefreshProducts();
        }
        private async Task RefreshProducts()
        {
            using (var client = new HttpClient())
            {
                FilterViewModel filter = new FilterViewModel(_filter);
                filter.clear();
                URLBuilder url = new URLBuilder(filter, "/api//Product/");
                url.URL += "&ShowMyOffers=true";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<ProductDto> result = JsonConvert.DeserializeObject<List<ProductDto>>(contents);
                Products.Clear();
                foreach (ProductDto p in result)
                {
                    ProductWrapper product = p.createProduct();
                    Products.Add(product);
                }
            }
        }
        public async Task AddOffer()
        {
            if(CreatedOffer.Name == null || CreatedOffer.Product == null  
                || CreatedOffer.Amount <= 0  || CreatedOffer?.Price <= 0)
            {
                CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);
                ErrorString = (string)Application.Current.FindResource("InvalidSellOfferError");
                return;
            }
            using (var client = new HttpClient())
            {
                CreatedOffer.ProductId = CreatedOffer.Product.Id;
                FilterViewModel filter = new FilterViewModel(_filter);
                filter.Name = _createdOffer.Product.Name;
                URLBuilder url = new URLBuilder(filter, controler);
                url.URL += "&ShowMyOffers=true";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<SellOfferDto> result = JsonConvert.DeserializeObject<List<SellOfferDto>>(contents);
                var totalAmount = result.Sum(offer => offer.Amount);
                if (CreatedOffer.Amount + totalAmount > CreatedOffer.Product.Stock)
                {
                    CreatedOffer = SellOfferWrapper.CreateSellOffer(_user);
                    ErrorString = (string)Application.Current.FindResource("StockError");
                    return;
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                SellOfferDto content = createOffer(_createdOffer);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                url = new URLBuilder(controler);
                var request2 = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")
                };
                request2.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response1 = await client.SendAsync(request2);
                if (response.IsSuccessStatusCode)
                {
                    await Load();
                    _createdOffer = SellOfferWrapper.CreateSellOffer(_user);

                }
                else
                {
                    ErrorString = (string)Application.Current.FindResource("InsertSellOfferError");
                    return;
                }
            }
            ErrorString = null;
        }
        public async Task UpdateOffer()
        {
            using (var client = new HttpClient())
            {
                FilterViewModel filter = new FilterViewModel(_filter);
                filter.clear();
                filter.Name = _selectedOffer.Product.Name;
                filter.SellerId = _authenticationUser.UserId;
                filter.Stock = "1";
                URLBuilder url = new URLBuilder(filter, "/api//Product/");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<ProductDto> result = JsonConvert.DeserializeObject<List<ProductDto>>(contents);
                if (result.Count() == 1)
                {
                    if (result.First().Stock < _selectedOffer.Amount)
                    {
                        ErrorString = (string)Application.Current.FindResource("StockError");
                        return;
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                SellOfferDto content = createOffer(_selectedOffer);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                url = new URLBuilder(controler);
                var request2 = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Put,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")
                };
                request2.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response2 = await client.SendAsync(request2);

                if (!response2.IsSuccessStatusCode)
                {
                    ErrorString = (string)Application.Current.FindResource("InvalidBuyOfferError");
                    return;
                }
            }
            await Load();
            ErrorString = null;
        }
        public async Task DeleteOffer()
        {
            if (SelectedOffer != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    SellOfferDto content = createOffer(_selectedOffer);
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                    URLBuilder url = new URLBuilder(controler);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(url.URL),
                        Method = HttpMethod.Delete,
                        Content = new StringContent(json,
                                        Encoding.UTF8,
                                        "application/json")
                    };
                    request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                    var response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorString = (string)Application.Current.FindResource("DeleteProductError");
                        return;
                    }
                    await Load();
                }
                ErrorString = null;
            }
        }

        public bool CanModifyOffer()
        {
            if (_selectedOffer == null)
            {
                return false;
            }
            return true;
        }
        SellOfferDto createOffer(SellOfferWrapper offer)
        {
            SellOfferDto wrap = new SellOfferDto();
            wrap.Id = offer.Id;
            wrap.SellerId = _authenticationUser.UserId;
            wrap.Price = offer.Price;
            wrap.Amount = offer.Amount;
            wrap.Name = offer.Name;
            wrap.ProductId = offer.ProductId;
            ProductDto product = createProductDto(offer.Product);
            wrap.Product = product;
            return wrap;
        }
        ProductDto createProductDto(ProductWrapper productWrap)
        {
            ProductDto content = new ProductDto();
            ConditionDto condition = new ConditionDto();
            GenreDto genre = new GenreDto();
            ProductTypeDto productType = new ProductTypeDto();
            content.ConditionId = productWrap.ConditionId;
            content.GenreId = productWrap.GenreId;
            content.ProductTypeId = productWrap.ProductTypeId;
            content.Id = productWrap.Id;
            content.Name = productWrap.Name;
            content.ProductOwner = _authenticationUser.UserId;
            content.SoldCopies = 0;
            content.Stock = productWrap.Stock;
            return content;
        }
    }
}
