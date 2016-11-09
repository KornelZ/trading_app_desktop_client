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
            if(c == null)
            {
                dicCondition = new dic_condition();
                return;
            }
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
        public int UpdateWho
        {
            get { return dicCondition.Update_Who; }
            set { dicCondition.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return dicCondition.Update_Date; }
            set { dicCondition.Update_Date = value; }
        }
        public override string ToString()
        {
            if(Name != null)
            {
                return Name;
            }
            return "All/Any";
        }
    }
}
