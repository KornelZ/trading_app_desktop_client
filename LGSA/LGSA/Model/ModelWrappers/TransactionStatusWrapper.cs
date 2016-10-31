using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class TransactionStatusWrapper : Utility.BindableBase
    {
        private dic_Transaction_status dicTransactionStatus;
        public dic_Transaction_status DicTransactionStatus
        {
            get { return dicTransactionStatus; }
            set { dicTransactionStatus = value; Notify(); }
        }
        public int Id
        {
            get { return dicTransactionStatus.ID; }
        }
        public string Name
        {
            get { return dicTransactionStatus.name; }
            set { dicTransactionStatus.name = value; Notify(); }
        }
        public string OfferTransactionDescription
        {
            get { return dicTransactionStatus.offer_transaction_description; }
            set { dicTransactionStatus.offer_transaction_description = value; Notify(); }
        }
    }
}
