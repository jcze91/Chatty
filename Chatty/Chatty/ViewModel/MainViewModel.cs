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
        public static IHubProxy Proxy { get; set; }

        LoginViewModel Login { get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); ; } }

        public MainViewModel()
        {
            hubConnection = new HubConnection("http://localhost:64061/");
            Proxy = hubConnection.CreateHubProxy("MainHub");
            Proxy.On<string, string>("addNewMessageToPage", (name, message) => { Login.callback(name, message); });
            Proxy.On<string>("OnConnectionInfo", (name) => { Login.callback(name); });
            //Proxy.On<string, Object>("OnConnectionInfo", (arg, res) => { Utils.CallbackHandler.Handle(arg, res); });
            //Proxy.On<string[]>("OnError", (args) => { Login.O(args); });
            hubConnection.Start().Wait();
        }
    }
}
