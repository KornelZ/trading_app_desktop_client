using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _genre = new GenreWrapper(new dic_Genre());
            _condition = new ConditionWrapper(new dic_condition());
            _name = "";
            _price = "0";
            _rating = "2";
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
    }
}
