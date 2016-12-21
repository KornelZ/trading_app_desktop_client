using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class ProductTypeDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ProductTypeWrapper createProductType()
        {
            ProductTypeWrapper product_type = new ProductTypeWrapper(new LGSA.Model.dic_Product_type());
            product_type.Id = this.Id;
            product_type.Name = this.Name;
            return product_type;
        }
    }
}