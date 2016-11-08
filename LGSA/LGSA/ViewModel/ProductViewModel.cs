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

namespace LGSA.ViewModel
{

    public class ProductViewModel : BindableBase
    {
        ProductService _productService;
        BindableCollection<ProductWrapper> _products;
        FilterViewModel _filter;
        UserWrapper _user;
        public ProductViewModel (IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user)
        {
            _user = user;
            _productService = new ProductService(factory);
            _filter = filter;
            Products = new BindableCollection<ProductWrapper>();
        }
        public BindableCollection<ProductWrapper> Products
        {
            get { return _products; }
            set { _products = value; Notify(); }
        }
        public async Task GetProducts()
        {
            int rating = 1;
            int stock = 1;
            if (_filter.Rating != null)
            {
                rating = int.Parse(_filter.Rating);
            }
            if (_filter.Stock != null)
            {
                stock = int.Parse(_filter.Stock);
            }
            Expression<Func<product, bool>> predicate = p => p.Name.Contains(_filter.Name)
            && p.users.First_Name == _user.FirstName && p.users.Last_Name == _user.LastName &&
            p.rating >= rating && p.dic_condition.name.Contains(_filter.Condition.Name) &&
            p.dic_Genre.name.Contains(_filter.Genre.Name) && p.stock >= stock;

            var x = await _productService.GetData(predicate);
            Products.Clear();
            foreach (product p in x)
            {
                Products.Add(new ProductWrapper(p));
            }
        }


    }
}
