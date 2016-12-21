using LGSA.ViewModel;
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
        private UserWrapper buyer;
        private OfferStatusWrapper offerStatus;
        public buy_Offer BuyOffer
        {
            get { return buyOffer; }
            set { buyOffer = value; Notify(); }
        }
        public BuyOfferWrapper(buy_Offer b)
        {
            buyOffer = b;
            Price = (decimal?)b.price;
            Amount = b.amount;
            Name = b.name;
            SoldCopies = b.sold_copies;
        }

        public static BuyOfferWrapper CreateEmptyBuyOffer(UserWrapper _user)
        {
            return new BuyOfferWrapper(new buy_Offer())
            {
                BuyerId = _user.Id,
                UpdateDate = DateTime.Now,
                UpdateWho = _user.Id,
                Product = new ProductWrapper(new product())
                {
                    OwnerId = _user.Id,
                    UpdateDate = DateTime.Now,
                    UpdateWho = _user.Id,
                    Genre = new GenreWrapper(new dic_Genre()),
                    Condition = new ConditionWrapper(new dic_condition()),
                    ProductType = new ProductTypeWrapper(new dic_Product_type()),
                    Stock = 0
                },
                StatusId = (int)TransactionState.Created,
            };
        }
        public static BuyOfferWrapper CreateBuyOffer(buy_Offer b)
        {
            var wrapper = new BuyOfferWrapper(b);
            wrapper.Buyer = new UserWrapper(b.users);
            wrapper.Product = new ProductWrapper(b.product);
            if(b.product.dic_condition != null)
            {
                wrapper.Product.Condition = new ConditionWrapper(b.product.dic_condition);
            }
            if(b.product.dic_Genre != null)
            {
                wrapper.Product.Genre = new GenreWrapper(b.product.dic_Genre);
            }
            if(b.product.dic_Product_type != null)
            {
                wrapper.Product.ProductType = new ProductTypeWrapper(b.product.dic_Product_type);
            }
            wrapper.UpdateDate = DateTime.Now;
            wrapper.UpdateWho = b.buyer_id;
            wrapper.OfferStatus = new OfferStatusWrapper(b.dic_Offer_status);

            return wrapper;
        }
        public int Id
        {
            get { return buyOffer.ID; }
            set { buyOffer.ID = value; Notify(); }
        }
        public int BuyerId
        {
            get { return buyOffer.buyer_id; }
            set { buyOffer.buyer_id = value; Notify(); }
        }
        public Nullable<decimal> Price
        {
            get { return (decimal?)buyOffer.price; }
            set { buyOffer.price = (double?)value; Notify(); }
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
            set { buyOffer.product_id = value; Notify(); }
        }
        public int StatusId
        {
            get { return buyOffer.status_id; }
            set { buyOffer.status_id = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return buyOffer.Update_Who; }
            set { buyOffer.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return buyOffer.Update_Date; }
            set { buyOffer.Update_Date = value; }
        }
        public ProductWrapper Product
        {
            get { return product; }
            set
            {
                product = value;
                buyOffer.product = product.Product;
                Notify();
            }
        }
        public UserWrapper Buyer
        {
            get { return buyer; }
            set
            {
                buyer = value;
                buyOffer.users = buyer.User;
                Notify();
            }
        }
        public OfferStatusWrapper OfferStatus
        {
            get { return offerStatus; }
            set
            {
                offerStatus = value;
                buyOffer.dic_Offer_status = offerStatus.DicOfferStatus;
                Notify();
            }
        }

        public void NullNavigationProperties()
        {
            this.BuyOffer.dic_Offer_status = null;
            this.BuyOffer.product = null;
            this.BuyOffer.users = null;
            this.BuyOffer.users1 = null;
        }
    }
}
