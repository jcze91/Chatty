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

        public int Login(string username, string password)
        {
            return Startup.container.Resolve<UserContractClient>().Login(username, password);
        }

        public bool SignIn(string username, string lastname, string firstname, string password, string email)
        {
            var res = Startup.container.Resolve<UserContractClient>().Insert(new User()
            {
                Username = username,
                Lastname = lastname,
                Firstname = firstname,
                Password = password,
                Email = email
            });
            return res == null;
        }

        public dynamic Execute(string[] args)
        {
            return null;
        }
    }
}