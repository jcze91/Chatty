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
        private Runtime runtime { get { return Startup.container.Resolve<Runtime>(); } }

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
            var res = Startup.container.Resolve<UserService>().SearchFor(x => x.Username == username && x.Password == password && x.isEnable).SingleOrDefault();
            return res == null ? -1 : res.Id;
        }

        public bool SignUp(string username, string lastname, string firstname, string password, string email)
        {
            var res = Startup.container.Resolve<UserService>().Insert(new User()
            {
                Username = username,
                Lastname = lastname,
                Firstname = firstname,
                Password = password,
                Email = email,
                isEnable = true
            });
            return res != null;
        }

        public void Execute(string[] args)
        {
            var result = runtime.Invoke(args);

            /**
             * FAIL
             */
            if (result == null || result is bool && !((bool)result))
                Clients.Caller.OnError(args);
            /**
             * SUCCESS
             */
            else
                Clients.All.Callback(args[0], result);
        }
    }
}