using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using BackOffice.Utils;
using Microsoft.Practices.Unity;
using BackOffice.UserProxy;

namespace BackOffice.Hubs
{
    public class MainHub : Hub
    {
        public MainHub() { }

        public override System.Threading.Tasks.Task OnConnected()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            var request = Context.Request;
            var uid = Guid.NewGuid().ToString().sha1();
            Clients.Client(Context.ConnectionId).OnConnectionInfo(uid);
            return base.OnConnected();
        }

        public bool Login(string username, string password)
        {
            return Startup.container.Resolve<UserContractClient>().Login(username, password);
        }

        public dynamic Execute(string[] args)
        {
            return null;
        }
    }
}