using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class ConditionWrapper : Utility.BindableBase
    {
        private dic_condition dicCondition;

        public dic_condition DicCondition
        {
            get { return dicCondition; }
            set { dicCondition = value; Notify(); }
        }
        public ConditionWrapper(dic_condition c)
        {
            dicCondition = c;
            Name = c.name;
        }
        public int Id
        {
            get { return dicCondition.ID; }
        }
        public string Name
        {
            get { return dicCondition.name; }
            set { dicCondition.name = value; Notify(); }
        }
    }
}
