using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.ViewModel
{
    public class SignInViewModel : Utils.BaseNotify
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { SetField(ref username, value, "Username"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetField(ref password, value, "Password"); }
        }
    }
}
