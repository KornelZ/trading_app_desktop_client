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
    }
}
