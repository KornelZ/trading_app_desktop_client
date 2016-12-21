using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class BuyOfferDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int BuyerId { get; set; }
        public decimal? Price { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Amount { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public UserDto User { get; set; }

        public BuyOfferWrapper createBuyOffer()
        {
            BuyOfferWrapper wrap = new BuyOfferWrapper(new LGSA.Model.buy_Offer());
            wrap.Id = this.Id;
            wrap.BuyerId = this.BuyerId;
            wrap.Price = this.Price;
            wrap.Amount = this.Amount;
            wrap.Name = this.Name;
            wrap.Buyer= new UserWrapper(new LGSA.Model.users());
            wrap.Buyer.Login = this.User.UserName;
            wrap.Buyer.Rating = this.User.Rating;
            wrap.ProductId = this.ProductId;
            wrap.Product = this.Product.createProduct();
            return wrap;
        }
    }
}