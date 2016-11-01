using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class BuyOfferWrapper : Utility.BindableBase
    {
        private buy_Offer buyOffer;
        private ProductWrapper product;
        private UserWrapper offerCreator;
        private UserWrapper buyer;
        private OfferStatusWrapper offerStatus;
        public buy_Offer BuyOffer
        {
            get { return buyOffer; }
            set { buyOffer = value; Notify(); }
        }

        public int Id
        {
            get { return buyOffer.ID; }
        }
        public int BuyerId
        {
            get { return buyOffer.buyer_id; }
        }
        public Nullable<decimal> Price
        {
            get { return (decimal)buyOffer.price; }
            set { buyOffer.price = (double)value; Notify(); }
        }
        public int Amount
        {
            get { return buyOffer.amount; }
            set { buyOffer.amount = value; Notify(); }
        }
        public string Name
        {
            get { return buyOffer.name; }
            set { buyOffer.name = value; Notify(); }
        }
        public int SoldCopies
        {
            get { return buyOffer.sold_copies; }
            set { buyOffer.sold_copies = value; Notify(); }
        }
        public int ProductId
        {
            get { return buyOffer.product_id; }
        }
        public int StatusId
        {
            get { return buyOffer.status_id; }
        }

        public ProductWrapper Product
        {
            get { return product; }
            set { product = value; Notify(); }
        }
        public UserWrapper OfferCreator
        {
            get { return offerCreator; }
            set { offerCreator = value;  Notify(); }
        }
        public UserWrapper Buyer
        {
            get { return buyer; }
            set { buyer = value; Notify(); }
        }
        public OfferStatusWrapper OfferStatus
        {
            get { return offerStatus; }
            set { offerStatus = value; Notify(); }
        }
    }
}
