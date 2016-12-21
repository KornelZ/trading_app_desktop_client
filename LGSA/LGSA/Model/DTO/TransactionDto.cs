using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class TransactionDto
    {
        [Required]
        public BuyOfferDto BuyOffer { get; set; }
        [Required]
        public SellOfferDto SellOffer { get; set; }    
        public int? Rating { get; set; }
    }
}