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
        String _stock;

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
        public String Price
        {
            get { return _price; }
            set { _price = value; Notify(); }
        }

        public String Name {
            get { return _name; }
            set { _name = value; Notify(); }
        }

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

    }
}
