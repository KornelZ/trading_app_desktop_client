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
        public Nullable<int> GenreId
        {
            get { return product.genre_id; }
        }
        public Nullable<int> ProductTypeId
        {
            get { return product.product_type_id; }
        }
        public Nullable<int> ConditionId
        {
            get { return product.condition_id; }
        }
        public GenreWrapper Genre
        {
            get { return genre; }
            set { genre = value; Notify(); }
        }
        public ConditionWrapper Condition
        {
            get { return condition; }
            set { condition = value; Notify(); }
        }
        public ProductTypeWrapper ProductType
        {
            get { return productType; }
            set { productType = value; Notify(); }
        }
        public UserWrapper Owner
        {
            get { return owner; }
            set { owner = value; Notify(); }
        }
    }
}
