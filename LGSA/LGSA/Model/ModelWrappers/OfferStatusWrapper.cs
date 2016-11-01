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
    }
}
