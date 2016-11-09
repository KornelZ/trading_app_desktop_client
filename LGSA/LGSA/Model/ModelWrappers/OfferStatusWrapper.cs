using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class OfferStatusWrapper : Utility.BindableBase
    {
        private dic_Offer_status dicOfferStatus;
        public dic_Offer_status DicOfferStatus
        {
            get { return dicOfferStatus; }
            set { dicOfferStatus = value; Notify(); }
        }
        public OfferStatusWrapper(dic_Offer_status o)
        {
            if(o == null)
            {
                dicOfferStatus = new dic_Offer_status();
                return;
            }
            dicOfferStatus = o;
            Name = o.name;
            OfferStatusDescription = o.offer_status_description;
        }
        public int Id
        {
            get { return dicOfferStatus.ID; }
        }
        public string Name
        {
            get { return dicOfferStatus.name; }
            set { dicOfferStatus.name = value; Notify(); }
        }
        public string OfferStatusDescription
        {
            get { return dicOfferStatus.offer_status_description; }
            set { dicOfferStatus.offer_status_description = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return dicOfferStatus.Update_Who; }
            set { dicOfferStatus.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return dicOfferStatus.Update_Date; }
            set { dicOfferStatus.Update_Date = value; }
        }
    }
}
