using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class TransactionWrapper : Utility.BindableBase
    {
        private transactions transaction;
        private UserWrapper seller;
        private UserWrapper buyer;
        private BuyOfferWrapper buyOffer;
        private SellOfferWrapper sellOffer;
        private TransactionStatusWrapper transactionStatus;
        public transactions Transaction
        {
            get { return transaction; }
            set { transaction = value; Notify(); }
        }
        public TransactionWrapper(transactions t)
        {
            transaction = t;
            TransactionDate = t.transaction_Date;
        }
        public int Id
        {
            get { return transaction.ID; }
        }
        public int SellerId
        {
            get { return transaction.seller_id; }
        }
        public int BuyerId
        {
            get { return transaction.buyer_id; }
        }
        public int BuyOfferId
        {
            get { return transaction.buy_offer_id; }
        }
        public int SellOfferId
        {
            get { return transaction.sell_offer_id; }
        }
        public System.DateTime TransactionDate
        {
            get { return transaction.transaction_Date; }
            set { transaction.transaction_Date = value; Notify(); }
        }
        public int StatusId
        {
            get { return transaction.status_id; }
        }

        public UserWrapper Seller
        {
            get { return seller; }
            set { seller = value; Notify(); }
        }
        public UserWrapper Buyer
        {
            get { return buyer; }
            set { buyer = value; Notify(); }
        }
        public BuyOfferWrapper BuyOffer
        {
            get { return buyOffer; }
            set { buyOffer = value; Notify(); }
        }
        public SellOfferWrapper SellOffer
        {
            get { return sellOffer; }
            set { sellOffer = value; Notify(); }
        }
        public TransactionStatusWrapper TransactionStatus
        {
            get { return transactionStatus; }
            set { transactionStatus = value; Notify(); }
        }
                
    }
}
