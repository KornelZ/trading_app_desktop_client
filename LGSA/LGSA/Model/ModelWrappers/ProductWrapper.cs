using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
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
            Rating = p.rating;
            Stock = p.stock;
            SoldCopies = p.sold_copies;
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
                product.dic_Genre = genre.DicGenre;
                Notify();
            }
        }
        public ConditionWrapper Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                product.dic_condition = condition.DicCondition;
                Notify();
            }
        }
        public ProductTypeWrapper ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                product.dic_Product_type = productType.DicProductType;
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
    }
}
