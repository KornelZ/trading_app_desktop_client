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

namespace LGSA.ViewModel
{

    public class ProductViewModel : BindableBase, IViewModel
    {
        private ProductService _productService;
        private BindableCollection<ProductWrapper> _products;
        private FilterViewModel _filter;
        private UserWrapper _user;
        private ProductWrapper _selectedProduct;
        private ProductWrapper _createdProduct;

        private AsyncRelayCommand _generateXMLReport;
        private AsyncRelayCommand _updateCommand;
        private DictionaryViewModel _dictionaryVM;


        public ProductViewModel (IUnitOfWorkFactory factory, FilterViewModel filter, UserWrapper user, DictionaryViewModel dic)
        {
            _dictionaryVM = dic;
            _user = user;
            _productService = new ProductService(factory);
            _filter = filter;
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
                int a;
            }
        }
        public async Task Load()
        {
            String genre = "";
            String conditon = "";
            int rating = 1;
            int stock = 1;
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
            if (_filter.Stock != null)
            {
                stock = int.Parse(_filter.Stock);
            }
            Expression<Func<product, bool>> predicate = p => p.product_owner == _user.Id && p.stock > 0;

            var x = await _productService.GetData(predicate);
            Products.Clear();
            foreach (product p in x)
            {
                ProductWrapper product = ProductWrapper.CreateProduct(p);
                Products.Add(product);
            }
        }

        public async Task AddProduct()
        {
            if (_createdProduct.Name == null || _createdProduct.Stock <= 0)
            {
                return;
            }
            _createdProduct.NullNavigationProperties();
            bool offerAdded = await _productService.Add(_createdProduct.Product);

            if (offerAdded == true)
            {
                Products.Add(_createdProduct);
                _createdProduct = ProductWrapper.CreateEmptyProduct(_user);
            }
        }

        public async Task Update()
        {
            if (SelectedProduct != null)
            {
                await _productService.Update(_selectedProduct.Product);
            }

        }


    }
}
