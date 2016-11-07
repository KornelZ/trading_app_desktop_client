using LGSA.Model.ModelWrappers;
using LGSA.Model.Services;
using LGSA.Model.UnitOfWork;
using LGSA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.ViewModel
{
    public class DictionaryViewModel : BindableBase
    {
        private GenreService _genreService;
        private ConditionService _conditionService;
        private ProductTypeService _productTypeService;

        private BindableCollection<GenreWrapper> _genres;
        private BindableCollection<ConditionWrapper> _conditions;
        private BindableCollection<ProductTypeWrapper> _productTypes;
        public DictionaryViewModel(IUnitOfWorkFactory _factory)
        {
            _genreService = new GenreService(_factory);
            _conditionService = new ConditionService(_factory);
            _productTypeService = new ProductTypeService(_factory);

            Genres = new BindableCollection<GenreWrapper>();
            Conditions = new BindableCollection<ConditionWrapper>();
            ProductTypes = new BindableCollection<ProductTypeWrapper>();
        }

        public BindableCollection<GenreWrapper> Genres
        {
            get { return _genres; }
            set { _genres = value; Notify(); }
        }
        public BindableCollection<ConditionWrapper> Conditions
        {
            get { return _conditions; }
            set { _conditions = value; Notify(); }
        }
        public BindableCollection<ProductTypeWrapper> ProductTypes
        {
            get { return _productTypes; }
            set { _productTypes = value; Notify(); }
        }

        public async Task LoadDictionaries()
        {
            var genres = await _genreService.GetData(null);
            foreach(var g in genres)
            {
                Genres.Add(new GenreWrapper(g));
            }

            var conditions = await _conditionService.GetData(null);
            foreach(var c in conditions)
            {
                Conditions.Add(new ConditionWrapper(c));
            }

            var productTypes = await _productTypeService.GetData(null);
            foreach(var p in productTypes)
            {
                ProductTypes.Add(new ProductTypeWrapper(p));
            }
        }
    }
}
