using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class UserAuthenticationWrapper : Utility.BindableBase
    {
        private users_Authetication userAuthentication;
        private UserWrapper user;
        public users_Authetication UserAuthentication
        {
            get { return userAuthentication; }
            set { userAuthentication = value; Notify(); }
        }
        public UserAuthenticationWrapper(users_Authetication u)
        {
            userAuthentication = u;
        }
        public int Id
        {
            get { return userAuthentication.ID; }
            set { UserAuthentication.ID = value; Notify(); }
        }
        public int UserId
        {
            get { return userAuthentication.User_id; }
            set { userAuthentication.User_id = value; Notify(); }
        }
        public string Password
        {
            get { return userAuthentication.password; }
            set { userAuthentication.password = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return userAuthentication.Update_Who; }
            set { userAuthentication.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return userAuthentication.Update_Date; }
            set { userAuthentication.Update_Date = value; }
        }
        public UserWrapper User
        {
            get { return user; }
            set
            {
                user = value;
                userAuthentication.users1 = user.User;
                Notify();
            }
        }
    }
}
