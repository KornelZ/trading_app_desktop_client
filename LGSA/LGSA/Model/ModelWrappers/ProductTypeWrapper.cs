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
        public int Id
        {
            get { return dicProductType.ID; }
        }
        public string Name
        {
            get { return dicProductType.name; }
            set { dicProductType.name = value; Notify(); }
        }
    }
}
