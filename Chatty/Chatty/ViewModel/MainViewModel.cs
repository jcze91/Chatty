using GalaSoft.MvvmLight;
using Microsoft.AspNet.SignalR.Client;
using System.Diagnostics;

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
    public class MainViewModel : ViewModelBase
    {
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

            var hubConnection = new HubConnection("http://localhost:64061/");
            IHubProxy stockTickerHubProxy = hubConnection.CreateHubProxy("ChatHub");
            stockTickerHubProxy.On<string, string>("addNewMessageToPage", (name, message) => { callback(name, message); });
            hubConnection.Start().Wait();
        }

        private static void callback(string name, string message)
        {
            Debug.WriteLine(name + " : " + message);
        }



    }
}