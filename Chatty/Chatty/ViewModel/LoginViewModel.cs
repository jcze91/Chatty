using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.AspNet.SignalR.Client;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Chatty.Utils;
using System;
using Microsoft.Practices.ServiceLocation;

namespace Chatty.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginViewModel : Utils.BaseNotify
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

        private bool isLogging = false;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public LoginViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private ICommand _loginCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new RelayCommand(() => Login(), () => canLog);
                return _loginCommand;
            }
        }

        private ICommand _createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                    _createCommand = new RelayCommand(() => OnCreate(EventArgs.Empty));
                return _createCommand;
            }
        }

        private bool canLog { get { return !isLogging && !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password); } }

        async private void Login()
        {
            isLogging = true;
            var res = await MainViewModel.Proxy.Invoke<int>("Login", new object[] { username, password.sha1() });
            isLogging = false;
            OnLogInfo(new LoginEventArgs() { UserId = res, Username = username, Logged = res != -1 });
        }

        public void callback(string name, string message = null)
        {
            Debug.WriteLine(name + " : " + message);
        }






        public event LoginEventHandler Logged;
        protected virtual void OnLogInfo(LoginEventArgs e)
        {
            LoginEventHandler handler = Logged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler Create;
        protected virtual void OnCreate(EventArgs e)
        {
            EventHandler handler = Create;
            if (handler != null)
                handler(this, e);
        }

    }

    public class LoginEventArgs : EventArgs
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool Logged { get; set; }
    }

    public delegate void LoginEventHandler(Object sender, LoginEventArgs e);

}