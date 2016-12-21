using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Model.Services;
using LGSA.Model.UnitOfWork;
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
    public class BuyOfferViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private UserAuthenticationWrapper _authenticationUser;
        private BuyOfferService _buyOfferService;
        private readonly string controller = "/api//BuyOffer/";
        private BindableCollection<BuyOfferWrapper> _buyOffers;

        private BuyOfferWrapper _createdOffer;
        private BuyOfferWrapper _selectedOffer;

        private AsyncRelayCommand _updateCommand;
        private AsyncRelayCommand _deleteCommand;
        private FilterViewModel _filter;

        private string _errorString;
        public BuyOfferViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user, UserAuthenticationWrapper authenticationUser)
        {
            _authenticationUser = authenticationUser;
            _user = user;
            _buyOfferService = new BuyOfferService(factory);
            BuyOffers = new BindableCollection<BuyOfferWrapper>();
            CreatedOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);

            _filter = filter;

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
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; Notify(); }
        }
        public async Task Load()
        {
            using (var client = new HttpClient())
            {

                //var predicate = CreateFilter();
                URLBuilder url = new URLBuilder(_filter, controller);
                url.URL += "&ShowMyOffers=true";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<BuyOfferDto> result = JsonConvert.DeserializeObject<List<BuyOfferDto>>(contents);
                //var x = await _productService.GetData(predicate);
                BuyOffers.Clear();
                foreach (BuyOfferDto bo in result)
                {

                    BuyOfferWrapper boffer = bo.createBuyOffer();
                    BuyOffers.Add(boffer);
                }
            }

            /*
            Expression<Func<buy_Offer, bool>> filter = CreateFilter();
            var offers = await _buyOfferService.GetData(filter);
            BuyOffers.Clear();
            foreach(var o in offers)
            {
                var product = ProductWrapper.CreateProduct(o.product);
                BuyOffers.Add(new BuyOfferWrapper(o)
                {
                    Product = product,
                });
            }*/
        }

        private Expression<Func<buy_Offer, bool>> CreateFilter()
        {
            String genre = "";
            String conditon = "";
            int rating = 1;
            int stock = 1;
            double price = 1;
            if (!_filter.Condition.Name.Equals("All/Any"))
            {
                conditon = _filter.Condition.Name;
            }
            if (!_filter.Genre.Name.Equals("All/Any"))
            {
                genre = _filter.Genre.Name;
            }
            if (_filter.Rating != null)
            {
                rating = int.Parse(_filter.Rating);
            }
            if (_filter.Price != null)
            {
                price = double.Parse(_filter.Price);
            }
            if (_filter.Stock != null)
            {
                stock = int.Parse(_filter.Stock);
            }

            Expression<Func<buy_Offer, bool>> filter = b => b.buyer_id == _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }
        public async Task AddOffer()
        {
            if(_createdOffer.Name == null || _createdOffer.Name == "" || _createdOffer.Product.Name == null || CreatedOffer.Amount <= 0 || CreatedOffer?.Price <= 0)
            {
                ErrorString = (string)Application.Current.FindResource("InvalidBuyOfferError");
                return;
            }
            CreatedOffer.Product.CheckForNull();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                BuyOfferDto content = createOffer(_createdOffer);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                //var predicate = CreateFilter();
                URLBuilder url = new URLBuilder(controller);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);


                //bool offerAdded = await _productService.Add(_createdProduct.Product);

                if (response.IsSuccessStatusCode)
                {
                    BuyOffers.Add(_createdOffer);
                    _createdOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);
                }
                else
                {
                    ErrorString = (string)Application.Current.FindResource("InvalidBuyOfferError");
                    return;
                }
            }
            /*bool offerAdded = await _buyOfferService.Add(_createdOffer.BuyOffer);

            if(offerAdded == true)
            {
                BuyOffers.Add(_createdOffer);
                _createdOffer = BuyOfferWrapper.CreateEmptyBuyOffer(_user);
            }
            else
            {
                ErrorString = (string)Application.Current.FindResource("InsertBuyOfferError");
                return;
            }*/
            ErrorString = null;
        }
        public async Task UpdateOffer()
        {

            if (SelectedOffer != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    BuyOfferDto content = createOffer(_selectedOffer);

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                    //var predicate = CreateFilter();
                    URLBuilder url = new URLBuilder(controller);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(url.URL),
                        Method = HttpMethod.Put,
                        Content = new StringContent(json,
                                        Encoding.UTF8,
                                        "application/json")
                    };
                    request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                    var response = await client.SendAsync(request);

                    //bool success = await _productService.Update(_selectedProduct.Product);
                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorString = (string)Application.Current.FindResource("InvalidBuyOfferError");
                        return;
                    }
                }
                await Load();
                ErrorString = null;
            }

            
        }
        public async Task DeleteOffer()
        {
            if (SelectedOffer != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    BuyOfferDto content = createOffer(_selectedOffer);

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                    //var predicate = CreateFilter();
                    URLBuilder url = new URLBuilder(controller);
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

                    //bool success = await _productService.Update(_selectedProduct.Product);
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
            if(_selectedOffer == null)
            {
                return false;
            }
            return true;
        }

        BuyOfferDto createOffer(BuyOfferWrapper offer)
        {
            BuyOfferDto wrap = new BuyOfferDto();
            wrap.Id = offer.Id;
            wrap.BuyerId = _authenticationUser.UserId;
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
            //condition.Id = productWrap.Condition.Id;
            //condition.Name = productWrap.Condition.Name;
            //content.Condition = condition;
            GenreDto genre = new GenreDto();
            //genre.Id = productWrap.Genre.Id;
            //genre.Description = productWrap.Genre.GenreDescription;
            //genre.Name = productWrap.Genre.Name;
            //content.Genre = genre;
            ProductTypeDto productType = new ProductTypeDto();
            //productType.Id = productWrap.ProductType.Id;
            //productType.Name = productWrap.ProductType.Name;
            //content.ProductType = productType;
            content.ConditionId = productWrap.ConditionId;
            content.GenreId = productWrap.GenreId;
            content.ProductTypeId = productWrap.ProductTypeId;
            content.Id = productWrap.Id;
            content.Name = productWrap.Name;
            content.ProductOwner = _authenticationUser.UserId;
            //content.Rating = productWrap.Rating;
            content.SoldCopies = 0;
            content.Stock = productWrap.Stock;
            return content;
        }
    }
}
