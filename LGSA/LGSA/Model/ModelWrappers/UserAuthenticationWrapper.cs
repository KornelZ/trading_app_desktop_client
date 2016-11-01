using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Id
        {
            get { return userAuthentication.ID; }
        }
        public int UserId
        {
            get { return userAuthentication.User_id; }
        }
        public string Password
        {
            get { return userAuthentication.password; }
        }
        public UserWrapper User
        {
            get { return user; }
            set { user = value; Notify(); }
        }
    }
}
