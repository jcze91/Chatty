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

        ChatViewModel Chat { get { return ServiceLocator.Current.GetInstance<ChatViewModel>(); } }

        public MainViewModel()
        {
            hubConnection = new HubConnection("http://localhost:4000/");
            Proxy = hubConnection.CreateHubProxy("MainHub");
            Proxy.On<string, int>("OnConnectionInfo", (info, uid) => { Chat.OnConnectionInfo(info, uid); });
            Proxy.On<string[], dynamic>("Callback", (args, res) => Chat.Callback(args, res));
            hubConnection.Start().Wait();
        }

        public event EventHandler OnWizz;
        public virtual void Wizz(EventArgs e)
        {
            EventHandler handler = OnWizz;
            if (handler != null)
                handler(this, e);
        }
    }
}
