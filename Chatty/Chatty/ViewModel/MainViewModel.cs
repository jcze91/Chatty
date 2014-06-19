using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.ViewModel
{
    public class MainViewModel : Utils.BaseNotify
    {
        private LoginViewModel login;

        public LoginViewModel Login
        {
            get { return login; }
            set { SetField(ref login, value, "Login"); }
        }

        private SignInViewModel signIn;

        public SignInViewModel SignIn
        {
            get { return signIn; }
            set { SetField(ref signIn, value, "SignIn"); }
        }

        public MainViewModel()
        {
            Login = new LoginViewModel();
            SignIn = new SignInViewModel();
        }
    }
}
