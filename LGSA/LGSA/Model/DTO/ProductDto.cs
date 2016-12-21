using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LGSA.Model.ModelWrappers;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductOwner { get; set; }
        [Required]
        public string Name { get; set; }
        public double? Rating { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Stock { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int SoldCopies { get; set; }
        public int? GenreId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? ConditionId { get; set; }
        public ConditionDto Condition { get; set; }
        public GenreDto Genre { get; set; }
        public ProductTypeDto ProductType { get; set; }

        public ProductWrapper createProduct()
        {
            ProductWrapper p = new ProductWrapper(new LGSA.Model.product());
            p.Id = this.Id;
            p.Product.product_owner = this.ProductOwner;
            p.Name = this.Name;
            p.Rating = this.Rating;
            p.Stock = this.Stock;
            p.SoldCopies = this.SoldCopies;
            p.GenreId = this.GenreId;
            p.ProductTypeId = this.ProductTypeId;
            p.ConditionId = this.ConditionId;
            if(this.Condition != null) p.Condition= this.Condition.createCondition();
            if (this.Genre != null) p.Genre = this.Genre.createGenre();
            if (this.ProductType != null) p.ProductType= this.ProductType.createProductType();

            return p;

        }

    }
}