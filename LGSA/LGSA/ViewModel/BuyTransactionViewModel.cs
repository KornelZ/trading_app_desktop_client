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
    public class BuyTransactionViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private FilterViewModel _filter;
        private BuyOfferService _buyOfferService;
        private ProductService _productService;
        private TransactionService _transactionService;
        private UserAuthenticationWrapper _authenticationUser;
        private BuyOfferWrapper _selectedOffer;
        private BindableCollection<BuyOfferWrapper> _offers;
        private AsyncRelayCommand _acceptCommand;
        private readonly string controller = "/api//BuyOffer/";

        private string _errorString;
        public BuyTransactionViewModel(IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user, UserAuthenticationWrapper authenticationUser)
        {
            _authenticationUser = authenticationUser;
            _buyOfferService = new BuyOfferService(factory);
            _productService = new ProductService(factory);
            _transactionService = new TransactionService(factory);
            _user = user;

            _filter = filter;
            Offers = new BindableCollection<BuyOfferWrapper>();
            SelectedOffer = new BuyOfferWrapper(new buy_Offer());

            AcceptCommand = new AsyncRelayCommand(execute => Accept(), canExecute => { return true; });
        }

        public AsyncRelayCommand AcceptCommand
        {
            get { return _acceptCommand; }
            set { _acceptCommand = value; Notify(); }
        }
        public BuyOfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value; Notify(); }
        }
        public BindableCollection<BuyOfferWrapper> Offers
        {
            get { return _offers; }
            set { _offers = value; Notify(); }
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
                URLBuilder url = new URLBuilder(_filter, controller);
                url.URL += "&ShowMyOffers=false";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<BuyOfferDto> result = JsonConvert.DeserializeObject<List<BuyOfferDto>>(contents);
                Offers.Clear();
                foreach (BuyOfferDto bo in result)
                {

                    BuyOfferWrapper boffer = bo.createBuyOffer();
                    Offers.Add(boffer);
                }
                /*var offers = await _buyOfferService.GetData(CreateFilter());
                Offers.Clear();

                foreach(var offer in offers)
                {
                    Offers.Add(BuyOfferWrapper.CreateBuyOffer(offer));
                }*/
            }
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

            Expression<Func<buy_Offer, bool>> filter = b => b.buyer_id != _user.Id && b.status_id != 3 &&
            b.product.Name.Contains(_filter.Name) && b.product.dic_condition.name.Contains(conditon) &&
            b.product.dic_Genre.name.Contains(genre) && b.price <= price && b.amount >= stock;
            return filter;
        }

        public async Task Accept()
        {
            string rating = "";
            RateWindow win = new RateWindow();
            win.ShowDialog();
            rating = win.Rating;

            SellOfferDto sOffer = new SellOfferDto();
            sOffer.Id = 0;
            sOffer.SellerId = _authenticationUser.UserId;
            sOffer.Price = (decimal?)SelectedOffer.Price;
            sOffer.Amount = SelectedOffer.Amount;
            sOffer.Name = "a";
            sOffer.ProductId = SelectedOffer.ProductId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                BuyOfferDto buyOffer = createOffer(SelectedOffer);
                TransactionDto transaction = new TransactionDto();
                transaction.BuyOffer = buyOffer;
                transaction.SellOffer = sOffer;
                if (rating == "")
                {
                    transaction.Rating = null;
                } else {
                    transaction.Rating = Convert.ToInt32(rating);
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
                var url = new URLBuilder("/AcceptBuyTransaction/");
                var request2 = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Post,
                    Content = new StringContent(json,
                                    Encoding.UTF8,
                                    "application/json")
                };
                request2.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request2);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorString = (string)Application.Current.FindResource("TransactionError");
                    return;
                }
                Offers.Remove(SelectedOffer);
            }
            /*
            product sellerProduct = null;
            var productQuery = await _productService.GetData(prod => prod.product_owner == _user.Id && prod.Name == SelectedOffer.Product.Name);
            if(productQuery.Count() != 0)
            {
                sellerProduct = productQuery.First();
            }
            if(sellerProduct == null)
            {
                ErrorString = (string)Application.Current.FindResource("TransactionError");
                return;
            }
            if(sellerProduct.stock < SelectedOffer.Amount)
            {
                ErrorString = (string)Application.Current.FindResource("TransactionError");
                return;
            }

            SelectedOffer.StatusId = (int)TransactionState.Finished;
            var sellOffer = new sell_Offer()
            {
                name = SelectedOffer.Name,
                Update_Who = _user.Id,
                Update_Date = DateTime.Now,
                seller_id = _user.Id,
                status_id = (int)TransactionState.Finished,
                product_id = sellerProduct.ID,
                price = (double)SelectedOffer.Price,
                amount = SelectedOffer.Amount,
            };
            SelectedOffer.NullNavigationProperties();
            SelectedOffer.Product.NullNavigationProperties();
            bool result = await _transactionService.AcceptBuyTransaction(sellOffer, SelectedOffer.BuyOffer,
                SelectedOffer.Product.Product, sellerProduct);
            if(result == false)
            {
                ErrorString = (string)Application.Current.FindResource("TransactionError");
                return;
            }
            ErrorString = null;
            Offers.Remove(SelectedOffer);*/
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
            //ProductDto product = createProductDto(offer.Product);
            //wrap.Product = product;
            return wrap;
        }
    }
}
