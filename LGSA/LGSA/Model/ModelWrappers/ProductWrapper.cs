using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    [Serializable]
    public class ProductWrapper : Utility.BindableBase
    {
        private product product;
        private GenreWrapper genre;
        private ProductTypeWrapper productType;
        private ConditionWrapper condition;
        private UserWrapper owner;
        public product Product
        {
            get { return product; }
            set { product = value; Notify(); }
        }

        public ProductWrapper(product p)
        {
            product = p;
            Name = p.Name;
            if(p.rating != null)
            {
                Rating = p.rating;
            }
            Stock = p.stock;
            SoldCopies = p.sold_copies;
        }

        public static ProductWrapper CreateProduct(product p)
        {
            var product = new ProductWrapper(p);
            if(p.dic_Genre != null)
            {
                product.Genre = new GenreWrapper(p.dic_Genre);
            }
            if(p.dic_condition != null)
            {
                product.Condition = new ConditionWrapper(p.dic_condition);
            }
            if(p.dic_Product_type != null)
            {
                product.ProductType = new ProductTypeWrapper(p.dic_Product_type);
            }

            return product;
        }
        
        public int Id
        {
            get { return product.ID; }
        }
        
        public string Name
        {
            get { return product.Name; }
            set { product.Name = value; Notify(); }
        }
        
        public Nullable<double> Rating
        {
            get { return product.rating; }
            set { product.rating = value; Notify(); }
        }
        
        public int Stock
        {
            get { return product.stock; }
            set { product.stock = value; Notify(); }
        }
        
        public int SoldCopies
        {
            get { return product.sold_copies; }
            set { product.sold_copies = value; Notify(); }
        }
        
        public int OwnerId
        {
            get { return product.product_owner; }
            set { product.product_owner = value; Notify(); }
        }
        
        public Nullable<int> GenreId
        {
            get { return product.genre_id; }
            set { product.genre_id = value; Notify(); }
        }
        
        public Nullable<int> ProductTypeId
        {
            get { return product.product_type_id; }
            set { product.product_type_id = value; Notify(); }
        }
        
        public Nullable<int> ConditionId
        {
            get { return product.condition_id; }
            set { product.condition_id = value; Notify(); }
        }
        
        public int UpdateWho
        {
            get { return product.Update_Who; }
            set { product.Update_Who = value; }
        }
        
        public DateTime UpdateDate
        {
            get { return product.Update_Date; }
            set { product.Update_Date = value; }
        }
        
        public GenreWrapper Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                if (genre != null && genre.Id != 0)
                {
                    product.genre_id = genre.Id;

                    product.dic_Genre = genre.DicGenre;
                }
                else
                {
                    product.genre_id = null;

                    product.dic_Genre = null;
                }
                Notify();
            }
        }
        
        public ConditionWrapper Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                if (condition != null && condition.Id != 0)
                {
                    product.condition_id = condition.Id;

                    product.dic_condition = condition.DicCondition;
                }
                else
                {
                    product.condition_id = null;

                    product.dic_condition = null;
                }
                Notify();
            }
        }
        public ProductTypeWrapper ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                if (productType != null && productType.Id != 0)
                {
                    product.product_type_id = productType.Id;

                    product.dic_Product_type = productType.DicProductType;
                }
                else
                {
                    product.product_type_id = null;

                    product.dic_Product_type = null;
                }
                Notify();
            }
        }
        public UserWrapper Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                product.users = owner.User;
                Notify();
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void NullNavigationProperties()
        {
            this.product.dic_condition = null;
            this.product.dic_Genre = null;
            this.product.dic_Product_type = null;
            this.product.users = null;
            this.product.users1 = null;
        }
        public void CheckForNull()
        {
            if(Genre.Id == 0)
            {
                Genre = null;
            }
            if(Condition.Id == 0)
            {
                Condition = null;
            }
            if(ProductType.Id == 0)
            {
                ProductType = null;
            }
        }
    }
}
