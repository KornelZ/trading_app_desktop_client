using LGSA.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LGSA.Utility
{
    class URLBuilder
    {
        private string url = "https://lgsa-server.azurewebsites.net";
        public URLBuilder(string controller)
        {
            url += controller;
        }
        public URLBuilder(FilterViewModel filter, string type)
        {
            NameValueCollection queryString = new NameValueCollection();

            
            switch (type)
            {
                case "/api//Product/":
                    if (filter.Condition.Name != "All/Any") queryString.Add("ConditionId", filter.Condition.Id.ToString());
                    if (filter.Genre.Name != "All/Any") queryString.Add("GenreId", filter.Genre.Id.ToString());
                    if (filter.ProductType.Name != "All/Any") queryString.Add("ProductTypeId", filter.ProductType.Id.ToString());
                    if (filter.Name != "") queryString.Add("Name", filter.Name);
                    if (filter.Rating != "") queryString.Add("Rating", filter.Rating);
                    if (filter.Stock != "") queryString.Add("Stock", filter.Stock);
                    if (filter.ProductOwner != null) queryString.Add("ProductOwner", filter.ProductOwner.ToString());
                    if (filter.SoldCopies != null) queryString.Add("SoldCopies", filter.SoldCopies.ToString());
                    if (filter.Id != null) queryString.Add("Id", filter.Id.ToString());

                    url += "/api//Product/";                
                    break;
                case "/api//BuyOffer/":
                    if (filter.Price != "") queryString.Add("Price", filter.Price);
                    if (filter.BuyerId != null) queryString.Add("BuyerId",filter.BuyerId.ToString());
                    if (filter.Name != "") queryString.Add("Name",filter.Name);
                    if (filter.Amount != null) queryString.Add("Amount", filter.Amount.ToString());
                    if (filter.Id != null) queryString.Add("Id", filter.Id.ToString());
                    url += "/api//BuyOffer/";
                    break;
                case "/api//SellOffer/":
                    if (filter.Price != "") queryString["Price"] = filter.Price;
                    if (filter.SellerId != null) queryString["SellerId"] = filter.SellerId.ToString();
                    if (filter.Amount != null) queryString["Amount"] = filter.Amount.ToString();
                    if (filter.Id != null) queryString["Id"] = filter.Id.ToString();
                    if (filter.Name != "") queryString["Name"] = filter.Name;
                    url += "/api//SellOffer/";
                    break;
                default:
                    break;
            }
            url += ToQueryString(queryString);
        }
        string ToQueryString(NameValueCollection collection)
        {
            var array = (from key in collection.AllKeys
                         from value in collection.GetValues(key)
                         select string.Format("{0}={1}", key, value)).ToArray();
            return "?" + string.Join("&", array);


        }
        public string URL { get { return url; } set { url = value; } }

    }

    
}
