using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class UserWrapper : Utility.BindableBase
    {
        private users user;
        public users User
        {
            get { return user; }
            set { user = value; Notify(); }
        }
        public UserWrapper(users u)
        {
            user = u;
            LastName = u.Last_Name;
            FirstName = u.First_Name;
        }
        public int Id
        {
            get { return user.ID; }
        }
        public string LastName
        {
            get { return user.Last_Name; }
            set { user.Last_Name = value; Notify(); }
        }
        public string FirstName
        {
            get { return user.First_Name; }
            set { user.First_Name = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return user.Update_Who; }
            set { user.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return user.Update_Date; }
            set { user.Update_Date = value; }
        }
    }
}
