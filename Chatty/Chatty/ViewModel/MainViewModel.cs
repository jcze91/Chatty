using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.AspNet.SignalR.Client;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Chatty.Utils;

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
    public class MainViewModel : Utils.BaseNotify
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


        HubConnection hubConnection;
        IHubProxy stockTickerHubProxy;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            hubConnection = new HubConnection("http://localhost:64061/");
            stockTickerHubProxy = hubConnection.CreateHubProxy("MainHub");
            stockTickerHubProxy.On<string, string>("addNewMessageToPage", (name, message) => { callback(name, message); });
            stockTickerHubProxy.On<string>("OnConnectionInfo", (name) => { callback(name); });
            hubConnection.Start().Wait();
        }

        private ICommand _loginCommand;

        public ICommand LogInCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new RelayCommand(() => Login());
                return _loginCommand;
            }
        }


        async private void Login()
        {
            var result = await stockTickerHubProxy.Invoke<bool>("Login", new object[] { username, password.sha1() });
        }

        private static void callback(string name, string message = null)
        {
            Debug.WriteLine(name + " : " + message);
        }



    }
}