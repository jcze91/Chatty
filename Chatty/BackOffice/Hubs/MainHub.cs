using BackOffice.Dbo;
using BackOffice.Services;
using BackOffice.Utils;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Threading;

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
            var srv = Startup.container.Resolve<UserService>();
            var res = srv.GetAll();
            var user = res.SingleOrDefault();
            return user == null ? -1 : user.Id;
        }

        public bool SignIn(string username, string lastname, string firstname, string password, string email)
        {
            var res = Startup.container.Resolve<UserService>().Insert(new User()
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