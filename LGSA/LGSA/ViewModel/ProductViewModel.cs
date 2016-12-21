using LGSA.Utility;
using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGSA.Model.UnitOfWork;
using LGSA.Model.Services;
using System.Linq.Expressions;
using LGSA.Model;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Windows;
using System.Net.Http;
using LGSA_Server.Model.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LGSA.ViewModel
{

    public sealed class ProductViewModel : BindableBase, IViewModel
    {
        private BindableCollection<ProductWrapper> _products;
        private FilterViewModel _filter;
        private UserWrapper _user;
        private ProductWrapper _selectedProduct;
        private ProductWrapper _createdProduct;
        private UserAuthenticationWrapper _authenticationUser;
        private readonly string controler = "/api//Product/";
        private AsyncRelayCommand _generateXMLReport;
        private AsyncRelayCommand _updateCommand;
        private DictionaryViewModel _dictionaryVM;

        private string _errorString;
        public ProductViewModel (FilterViewModel filter, UserWrapper user, DictionaryViewModel dic, UserAuthenticationWrapper authUser)
        {
            _dictionaryVM = dic;
            _user = user;
            _filter = filter;
            _authenticationUser = authUser;
            Products = new BindableCollection<ProductWrapper>();
            CreatedProduct = ProductWrapper.CreateEmptyProduct(_user);
            GenerateXMLReport = new AsyncRelayCommand(execute => GenerateXML(), canExecute => { return true; });
            UpdateCommand = new AsyncRelayCommand(execute => Update(), canExecute => { return true; });
        }

        public ProductWrapper SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; Notify(); }
        }
        public ProductWrapper CreatedProduct
        {
            get { return _createdProduct; }
            set { _createdProduct = value; Notify(); }
        }
        public AsyncRelayCommand UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = value; Notify(); }
        }
        public AsyncRelayCommand GenerateXMLReport
        {
            get { return _generateXMLReport; }
            set { _generateXMLReport = value;  Notify(); }
        } 
        public BindableCollection<ProductWrapper> Products
        {
            get { return _products; }
            set { _products = value; Notify(); }
        }
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; Notify(); }
        }
        public async Task GenerateXML()
        {
            DateTime saveNow = DateTime.Now;

            try
            {
                var xmlfromLINQ = new XElement("Products",
                            from p in Products
                            select new XElement("Product",
                                new XElement("Name", p.Name),
                                new XElement("Rating", p.Rating),
                                new XElement("Stock", p.Stock),
                                new XElement("SoldCopies", p.SoldCopies),
                                new XElement("Owner", _user.FirstName + " " + _user.LastName),
                                new XElement("Genre", _dictionaryVM.Genres.First(item => item.Id == p.GenreId),
                                new XElement("Product_Type", _dictionaryVM.ProductTypes.First(item => item.Id == p.ProductTypeId)),
                                new XElement("Condition", _dictionaryVM.Conditions.First(item => item.Id == p.ConditionId)))
                                ));
                String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reports\\Products" + saveNow.Date + ".xml";
                path = "report" + saveNow.Year + "-" + saveNow.Month + "-" + saveNow.Day + ".xml";
                xmlfromLINQ.Save(path);
            }catch(Exception e)
            {
                ErrorString = (string)Application.Current.FindResource("ReportGenerationError");
                return;
            }
            ErrorString = null;
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
                List<ProductDto> result = JsonConvert.DeserializeObject<List<ProductDto>>(contents);
                Products.Clear();
                foreach (ProductDto p in result)
                {
                    ProductWrapper product = p.createProduct();
                    Products.Add(product);
                }
            }
        }
        public async Task AddProduct()
        {
            if (_createdProduct.Name == null || _createdProduct.Name == "" || _createdProduct.Stock <= 0)
            {
                ErrorString = (string)Application.Current.FindResource("InvalidProductError");
                return;
            }
            _createdProduct.NullNavigationProperties();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ProductDto content = createProductDto(_createdProduct);
                
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                URLBuilder url = new URLBuilder(controler);
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
                if (response.IsSuccessStatusCode)
                {
                    Products.Add(_createdProduct);
                    _createdProduct = ProductWrapper.CreateEmptyProduct(_user);
                }
                else
                {
                    ErrorString = (string)Application.Current.FindResource("InsertProductError");
                    return;
                }
            }
            ErrorString = null;
        }

        public async Task Update()
        {
            if (SelectedProduct != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    ProductDto content = createProductDto(_selectedProduct);

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                    URLBuilder url = new URLBuilder(controler);
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
                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorString = (string)Application.Current.FindResource("UpdateProductError");
                        return;
                    }
                    if(SelectedProduct.Stock == 0)
                    {
                        Products.Remove(SelectedProduct);
                    }
                }
                ErrorString = null;
            }

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
            content.Rating = productWrap.Rating;
            content.SoldCopies = 0;
            content.Stock = productWrap.Stock;
            return content;
        }
    }
}
