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
        private UserWrapper offerCreator;
        private UserWrapper seller;
        private OfferStatusWrapper offerStatus;
        public sell_Offer SellOffer
        {
            get { return sellOffer; }
            set { sellOffer = value; Notify(); }
        }
        public int Id
        {
            get { return sellOffer.ID; }
        }
        public int SellerId
        {
            get { return sellOffer.seller_id; }
        }
        public Nullable<double> Price
        {
            get { return sellOffer.price; }
            set { sellOffer.price = value; Notify(); }
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
        public int BuyedCopies
        {
            get { return sellOffer.buyed_copies; }
            set { sellOffer.buyed_copies = value; Notify(); }
        }
        public int ProductId
        {
            get { return sellOffer.product_id; }
        }
        public int StatusId
        {
            get { return sellOffer.status_id; }
        }
        public ProductWrapper Product
        {
            get { return product; }
            set { product = value; Notify(); }
        }
        public UserWrapper OfferCreator
        {
            get { return offerCreator; }
            set { offerCreator = value; }
        }
        public UserWrapper Seller
        {
            get { return seller; }
            set { seller = value; Notify(); }
        }
        public OfferStatusWrapper OfferStatus
        {
            get { return offerStatus; }
            set { offerStatus = value; Notify(); }
        }
    }
}
