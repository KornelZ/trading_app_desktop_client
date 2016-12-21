using LGSA.Model;
using LGSA.Model.ModelWrappers;
using LGSA.Utility;
using LGSA_Server.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LGSA.ViewModel
{
    public class DictionaryViewModel : BindableBase, IViewModel
    {
        

        private BindableCollection<GenreWrapper> _genres;
        private BindableCollection<ConditionWrapper> _conditions;
        private BindableCollection<ProductTypeWrapper> _productTypes;
        public DictionaryViewModel()
        {
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
            using (var client = new HttpClient())
            {
                URLBuilder url = new URLBuilder("/api/Dictionary/");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url.URL),
                    Method = HttpMethod.Get,
                };
                var response = await client.SendAsync(request);
                var contents = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }
                DictionaryDto result = JsonConvert.DeserializeObject<DictionaryDto>(contents);
                IEnumerable<GenreDto> genres = result.Genres;
                Genres.Add(new GenreWrapper(generalGenre));
                foreach (GenreDto g in genres)
                {
                    GenreWrapper wrap = g.createGenre();
                    Genres.Add(wrap);
                }
                IEnumerable<ConditionDto> conditions = result.Conditions;
                Conditions.Add(new ConditionWrapper(generalCondition));
                foreach (ConditionDto c in conditions)
                {
                    Conditions.Add(c.createCondition());
                }
                IEnumerable<ProductTypeDto> productTypes = result.ProductTypes;

                ProductTypes.Add(new ProductTypeWrapper(generalProductType));
                foreach (var p in productTypes)
                {
                    ProductTypes.Add(p.createProductType());
                }
            }

            
           
        }
    }
}
