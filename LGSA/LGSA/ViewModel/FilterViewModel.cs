using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.ViewModel
{
    public class FilterViewModel : BindableBase
    {
        String _name;
        GenreWrapper _genre;
        String _price;
        String _rating;
        ConditionWrapper _condition;
        ProductTypeWrapper _productType;
        String _stock;
        int? _productOwner;
        int? _soldCopies;
        int? _id;
        int? _buyerId;
        int? _sellerId;
        int? _amount;
            
        public FilterViewModel(FilterViewModel f)
        {
            Name = f.Name;
            Genre = f.Genre;
            ProductType = f.ProductType;
            Price = f.Price;
            Rating = f.Rating;
            Condition = f.Condition;
            Stock = f.Stock;
            ProductOwner = f.ProductOwner;
            SoldCopies = f.SoldCopies;
            Id = f.Id;
            BuyerId = f.BuyerId;
            SellerId = f.SellerId;
            Amount = f.Amount;
        }
        public FilterViewModel()
        {
            dic_Genre dicGenre = new dic_Genre();
            dic_condition dicCondition = new dic_condition();
            dicGenre.name = "All/Any";
            dicCondition.name = "All/Any";
            _genre = new GenreWrapper(dicGenre);
            _condition = new ConditionWrapper(dicCondition);
            _name = "";
            _price = "100";
            _rating = "1";
            _stock = "1";
        }

        public int? ProductOwner { get { return _productOwner; } set { _productOwner = value; Notify(); } }

        public int? SoldCopies { get { return _soldCopies; } set { _soldCopies = value; Notify(); } }
        public int? Id { get { return _id; } set { _id = value; Notify(); } }
        public int? BuyerId { get { return _buyerId; } set { _buyerId = value; Notify(); } }
        public int? SellerId { get { return _sellerId; } set { _sellerId = value; Notify(); } }
        public int? Amount { get { return _amount; } set { _amount = value; Notify(); } }
        public String Price
        {
            get { return _price; }
            set { _price = value; Notify(); }
        }

        public String Name {
            get { return _name; }
            set { _name = value; Notify(); }
        }
        public ProductTypeWrapper ProductType { get { return _productType; } set { _productType = value; Notify(); } }
        public GenreWrapper Genre
        {
            get { return _genre; }
            set { _genre = value; Notify(); }
        }

        public String Rating
        {
            get { return _rating; }
            set { _rating = value; Notify(); }
        }

        public ConditionWrapper Condition
        {
            get { return _condition; }
            set { _condition = value; Notify(); }
        }

        public String Stock
        {
            get { return _stock; }
            set { _stock = value; Notify(); }
        }

        public decimal ParsedPrice()
        {
            decimal price = 0;
            decimal.TryParse(Price, out price);

            return price;
        }
        public double ParsedRating()
        {
            double rating = 0.0;
            double.TryParse(Rating, out rating);

            return rating;
        }
        public int ParsedStock()
        {
            int stock = 0;
            int.TryParse(Stock, out stock);

            return stock;
        }
        public void clear()
        {
            dic_Genre dicGenre = new dic_Genre();
            dic_Product_type dicProductType = new dic_Product_type();
            dic_condition dicCondition = new dic_condition();
            dicGenre.name = "All/Any";
            dicCondition.name = "All/Any";
            dicProductType.name = "All/Any";
            _productType = new ProductTypeWrapper(dicProductType);
            _genre = new GenreWrapper(dicGenre);
            _condition = new ConditionWrapper(dicCondition);
            _name = "";
            Price = "";
            _rating = "";
            _stock = "";
            _productOwner = null;
            _soldCopies = null;
            _id = null;
            _buyerId = null;
            _sellerId = null;
            _amount = null;
        }

    }
}
