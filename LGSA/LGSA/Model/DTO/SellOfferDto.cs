using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class SellOfferDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SellerId { get; set; }
        public decimal? Price { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Amount { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public UserDto User { get; set; }
        public SellOfferWrapper createSellOffer()
        {
            SellOfferWrapper offer = new SellOfferWrapper(new LGSA.Model.sell_Offer());
            offer.Id = this.Id;
            offer.SellerId = this.SellerId;
            offer.Price = this.Price;
            offer.Amount = this.Amount;
            offer.Name = this.Name;
            offer.Seller = new UserWrapper(new LGSA.Model.users());
            offer.Seller.Login = this.User.UserName;
            offer.Seller.Rating = this.User.Rating;
            offer.ProductId = this.ProductId;
            if (this.Product != null) offer.Product = this.Product.createProduct();
            return offer;
        }
    }
}