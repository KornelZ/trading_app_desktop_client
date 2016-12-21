using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Model.Services;
using LGSA.Model.UnitOfWork;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using LGSA_Server.Model.DTO;
using System.Net.Http.Headers;

namespace LGSA.ViewModel
{
    public class SellTransactionViewModel : BindableBase, IViewModel
    {
        private UserWrapper _user;
        private FilterViewModel _filter;
        private UserAuthenticationWrapper _authenticationUser;
        private SellOfferWrapper _selectedOffer;
        private BindableCollection<SellOfferWrapper> _offers;
        private AsyncRelayCommand _acceptCommand;
        private readonly string controler = "/api//SellOffer/";

        private string _errorString;
        public SellTransactionViewModel(FilterViewModel filter, UserWrapper user, UserAuthenticationWrapper authenticationUser)
        {
            _authenticationUser = authenticationUser;
            _user = user;
            _filter = filter;
            Offers = new BindableCollection<SellOfferWrapper>();
            SelectedOffer = new SellOfferWrapper(new sell_Offer());
            AcceptCommand = new AsyncRelayCommand(execute => Accept(), canExecute => { return true; });
        }

        public AsyncRelayCommand AcceptCommand
        {
            get { return _acceptCommand; }
            set { _acceptCommand = value; Notify(); }
        }
        public SellOfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set { _selectedOffer = value; Notify(); }
        }
        public BindableCollection<SellOfferWrapper> Offers
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
                URLBuilder url = new URLBuilder(_filter, controler);
                url.URL += "&ShowMyOffers=false";
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _authenticationUser.UserId.ToString(), _authenticationUser.Password))));
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                List<SellOfferDto> result = JsonConvert.DeserializeObject<List<SellOfferDto>>(contents);
                Offers.Clear();
                foreach (SellOfferDto bo in result)
                {
                    SellOfferWrapper boffer = bo.createSellOffer();
                    Offers.Add(boffer);
                }
            }
        }
        public async Task Accept()
        {
            string rating = "";
            RateWindow win = new RateWindow();
            win.ShowDialog();
            rating = win.Rating;

            BuyOfferDto bOffer = new BuyOfferDto();
            bOffer.Id = 0;
            bOffer.BuyerId = _authenticationUser.UserId;
            bOffer.Price = (decimal?)SelectedOffer.Price;
            bOffer.Amount = SelectedOffer.Amount;
            bOffer.Name = "a";
            bOffer.ProductId = SelectedOffer.ProductId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SellOfferDto sellOffer = createOffer(SelectedOffer);
                TransactionDto transaction = new TransactionDto();
                transaction.BuyOffer = bOffer;
                transaction.SellOffer = sellOffer;
                if (rating == "")
                {
                    transaction.Rating = null;
                }
                else
                {
                    transaction.Rating = Convert.ToInt32(rating);
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
                var url = new URLBuilder("/AcceptSellTransaction/");
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
            return wrap;
        }

        private void NullProductProperties(product buyerProduct)
        {
            buyerProduct.dic_condition = null;
            buyerProduct.dic_Genre = null;
            buyerProduct.dic_Product_type = null;
            buyerProduct.users = null;
            buyerProduct.users1 = null;
        }
    }
}
