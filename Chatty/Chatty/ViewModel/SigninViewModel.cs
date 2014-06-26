using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chatty.Utils;

namespace Chatty.ViewModel
{
    public class SignUpViewModel : Utils.BaseNotify
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { SetField(ref username, value, "Username"); }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { SetField(ref firstname, value, "Firstname"); }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { SetField(ref lastname, value, "Lastname"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetField(ref password, value, "Password"); }
        }

        private string password2;
        public string Password2
        {
            get { return password2; }
            set { SetField(ref password2, value, "Password2"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetField(ref email, value, "Email"); }
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                    _goBackCommand = new RelayCommand(() => OnGoBack(EventArgs.Empty), () => CanGoBack());
                return _goBackCommand;
            }
        }

        bool isSigningUp = false;
        private ICommand _signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                    _signUpCommand = new RelayCommand(() => SignUp(), () => CanSignIn());
                return _signUpCommand;
            }
        }

        async private void SignUp()
        {
            isSigningUp = true;
            var res = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-insert", username, lastname, firstname, email, password.sha1(), true.ToString() } });
            isSigningUp = false;
            if (res != null)
                OnSigned(EventArgs.Empty);
        }

        private bool CanGoBack()
        {
            return !isSigningUp;
        }

        private bool CanSignIn()
        {
            return !isSigningUp
                && !string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(firstname)
                && !string.IsNullOrWhiteSpace(lastname)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(password2)
                && !string.IsNullOrWhiteSpace(email)
                && password == password2;
        }

        public event EventHandler SignUpped;
        protected virtual void OnSigned(EventArgs e)
        {
            EventHandler handler = SignUpped;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler GoBack;
        protected virtual void OnGoBack(EventArgs e)
        {
            EventHandler handler = GoBack;
            if (handler != null)
                handler(this, e);
        }
    }

    public class SignUpEventArgs : EventArgs
    {
        public Dbo.User user { get; set; }
    }

    public delegate void SignUpEventHandler(Object sender, SignUpEventArgs e);
}
