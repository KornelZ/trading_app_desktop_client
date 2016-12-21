using LGSA.Model;
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
    public class DictionaryViewModel : BindableBase, IViewModel
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

        public async Task Load()
        {
            dic_Genre generalGenre = new dic_Genre();
            generalGenre.name = "All/Any";
            dic_Product_type generalProductType = new dic_Product_type();
            generalProductType.name = "All/Any";
            dic_condition generalCondition = new dic_condition();
            generalCondition.name = "All/Any";
            var genres = await _genreService.GetData(null);
            Genres.Add(new GenreWrapper(generalGenre));
            foreach (var g in genres)
            {
                Genres.Add(new GenreWrapper(g));
            }

            var conditions = await _conditionService.GetData(null);
            Conditions.Add(new ConditionWrapper(generalCondition));
            foreach (var c in conditions)
            {
                Conditions.Add(new ConditionWrapper(c));
            }

            var productTypes = await _productTypeService.GetData(null);
            ProductTypes.Add(new ProductTypeWrapper(generalProductType));
            foreach(var p in productTypes)
            {
                ProductTypes.Add(new ProductTypeWrapper(p));
            }
        }
    }
}
