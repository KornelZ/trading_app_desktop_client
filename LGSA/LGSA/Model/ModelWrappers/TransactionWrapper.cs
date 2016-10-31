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
        public transactions Transaction
        {
            get { return transaction; }
            set { transaction = value; Notify(); }
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
    }
}
