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
            set { transaction.seller_id = value; Notify(); }
        }
        public int BuyerId
        {
            get { return transaction.buyer_id; }
            set { transaction.buyer_id = value; Notify(); }
        }
        public int BuyOfferId
        {
            get { return transaction.buy_offer_id; }
            set { transaction.buy_offer_id = value; Notify(); }
        }
        public int SellOfferId
        {
            get { return transaction.sell_offer_id; }
            set { transaction.sell_offer_id = value; Notify(); }
        }
        public System.DateTime TransactionDate
        {
            get { return transaction.transaction_Date; }
            set { transaction.transaction_Date = value; Notify(); }
        }
        public int StatusId
        {
            get { return transaction.status_id; }
            set { transaction.status_id = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return transaction.Update_Who; }
            set { transaction.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return transaction.Update_Date; }
            set { transaction.Update_Date = value; }
        }
        public UserWrapper Seller
        {
            get { return seller; }
            set
            {
                seller = value;
                transaction.users1 = seller.User;
                Notify();
            }
        }
        public UserWrapper Buyer
        {
            get { return buyer; }
            set
            {
                buyer = value;
                transaction.users = buyer.User;
                Notify();
            }
        }
        public BuyOfferWrapper BuyOffer
        {
            get { return buyOffer; }
            set
            {
                buyOffer = value;
                transaction.buy_Offer = buyOffer.BuyOffer;
                Notify();
            }
        }
        public SellOfferWrapper SellOffer
        {
            get { return sellOffer; }
            set
            {
                sellOffer = value;
                transaction.sell_Offer = sellOffer.SellOffer;
                Notify();
            }
        }
        public TransactionStatusWrapper TransactionStatus
        {
            get { return transactionStatus; }
            set
            {
                transactionStatus = value;
                transaction.dic_Transaction_status = transactionStatus.DicTransactionStatus;
                Notify(); }
        }
                
    }
}
