using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class ProductTypeWrapper : Utility.BindableBase
    {
        private dic_Product_type dicProductType;
        public dic_Product_type DicProductType
        {
            get { return dicProductType; }
            set { dicProductType = value; Notify(); }
        }
        public ProductTypeWrapper(dic_Product_type p)
        {
            if(p == null)
            {
                dicProductType = new dic_Product_type();
                return;
            }
            dicProductType = p;
            Name = p.name;
        }
        public int Id
        {
            get { return dicProductType.ID; }
        }
        public string Name
        {
            get { return dicProductType.name; }
            set { dicProductType.name = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return dicProductType.Update_Who; }
            set { dicProductType.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return dicProductType.Update_Date; }
            set { dicProductType.Update_Date = value; }
        }

        public override string ToString()
        {
            if(Name != null)
            {
                return Name.ToString();
            }
            return "";
        }
    }
}
