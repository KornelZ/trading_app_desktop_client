using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class SellOfferWrapper : Utility.BindableBase
    {
        private sell_Offer sellOffer;
        private ProductWrapper product;
        private UserWrapper seller;
        private OfferStatusWrapper offerStatus;
        public sell_Offer SellOffer
        {
            get { return sellOffer; }
            set { sellOffer = value; Notify(); }
        }
        public SellOfferWrapper(sell_Offer s)
        {
            sellOffer = s;
            Price = (decimal?)s.price;
            Amount = s.amount;
            LastSellDate = s.last_sell_date;
            Name = s.name;
            BoughtCopies = s.buyed_copies;
        }
        public int Id
        {
            get { return sellOffer.ID; }
        }
        public int SellerId
        {
            get { return sellOffer.seller_id; }
            set { sellOffer.seller_id = value; Notify(); }
        }
        public Nullable<decimal> Price
        {
            get { return (decimal?)sellOffer.price; }
            set { sellOffer.price = (double?)value; Notify(); }
        }
        public int Amount
        {
            get { return sellOffer.amount; }
            set { sellOffer.amount = value; Notify(); }
        }
        public Nullable<System.DateTime> LastSellDate
        {
            get { return sellOffer.last_sell_date; }
            set { sellOffer.last_sell_date = value; Notify(); }
        }
        public string Name
        {
            get { return sellOffer.name; }
            set { sellOffer.name = value; Notify(); }
        }
        public int BoughtCopies
        {
            get { return sellOffer.buyed_copies; }
            set { sellOffer.buyed_copies = value; Notify(); }
        }
        public int ProductId
        {
            get { return sellOffer.product_id; }
            set { sellOffer.product_id = value; Notify(); }
        }
        public int StatusId
        {
            get { return sellOffer.status_id; }
            set { sellOffer.status_id = value; Notify(); }
        }
        public ProductWrapper Product
        {
            get { return product; }
            set
            {
                product = value;
                sellOffer.product = product.Product;
                Notify();
            }
        }
        public UserWrapper Seller
        {
            get { return seller; }
            set
            {
                seller = value;
                sellOffer.users = seller.User;
                Notify(); }
        }
        public OfferStatusWrapper OfferStatus
        {
            get { return offerStatus; }
            set
            {
                offerStatus = value;
                sellOffer.dic_Offer_status = offerStatus.DicOfferStatus;
                Notify();
            }
        }
    }
}
