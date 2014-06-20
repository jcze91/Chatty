using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using BackOffice.Utils;

namespace BackOffice.Hubs
{
    public class ChatHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            var request = Context.Request;
            var uid = Guid.NewGuid().ToString().sha1();
            Clients.Client(Context.ConnectionId).OnConnectionInfo(uid);
            return base.OnConnected();
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }   
    }
}