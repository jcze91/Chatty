using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.ViewModel
{
    public class MainViewModel : Utils.BaseNotify
    {
        HubConnection hubConnection;
        public static IHubProxy stockTickerHubProxy { get; set; }

        private static LoginViewModel Login { get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); ; } }

        public MainViewModel()
        {
            hubConnection = new HubConnection("http://localhost:64061/");
            stockTickerHubProxy = hubConnection.CreateHubProxy("MainHub");
            stockTickerHubProxy.On<string, string>("addNewMessageToPage", (name, message) => { Login.callback(name, message); });
            stockTickerHubProxy.On<string>("OnConnectionInfo", (name) => { Login.callback(name); });
            hubConnection.Start().Wait();
        }
    }
}
