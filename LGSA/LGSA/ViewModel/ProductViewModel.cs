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

        public ProductViewModel (IUnitOfWorkFactory factory)
        {
            _productService = new ProductService(factory);
            Products = new BindableCollection<ProductWrapper>();
        }
        public BindableCollection<ProductWrapper> Products
        {
            get { return _products; }
            set { _products = value; Notify(); }
        }
        public async Task GetProducts()
        {
            Expression<Func<product, bool>> predicate = u => 1 == 1;

            var x = await _productService.GetData(null);
            foreach (product p in x)
            {
                Products.Add(new ProductWrapper(p));
            }
        }


    }
}
