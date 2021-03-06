﻿using BackOffice.Dbo;
using BackOffice.Services;
using BackOffice.Utils;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace BackOffice.Hubs
{
    public class MainHub : Hub
    {
        private Runtime runtime { get { return Startup.container.Resolve<Runtime>(); } }

        public MainHub() { }

        //public override System.Threading.Tasks.Task OnConnected()
        //{
        //    //var identity = Thread.CurrentPrincipal.Identity;
        //    //var request = Context.Request;
        //    //var uid = Guid.NewGuid().ToString().sha1();
        //    //Clients.Client(Context.ConnectionId).OnConnectionInfo("connexion", uid);
        //    return base.OnConnected();
        //}

        public int Login(string username, string password)
        {
            var srv = Startup.container.Resolve<UserService>();
            var q = srv.SearchFor(x => x.Username == username && x.Password == password && x.isEnable);

            if (q == null)
                return -1;

            var res = q.SingleOrDefault();
            if (res != null)
            {
                var map = Startup.container.Resolve<ConcurrentDictionary<string, int>>();
                map.AddOrUpdate(Context.ConnectionId, res.Id, (cid, uid) => uid);
                Clients.All.OnConnectionInfo("connexion", res.Id);
                return res.Id;
            }
            else
                return -1;
        }

        public void LogOut(int uid)
        {
            try
            {
                var map = Startup.container.Resolve<ConcurrentDictionary<string, int>>();
                var entry = map.SingleOrDefault(x => x.Value == uid);
                int outValue;
                map.TryRemove(entry.Key, out outValue);
                Clients.All.OnConnectionInfo("deconnexion", uid);
            }
            catch { }
        }

        public bool IsUserOnline(int uid)
        {
            var map = Startup.container.Resolve<ConcurrentDictionary<string, int>>();
            return map.Count(x => x.Value == uid) > 0;
        }

        public dynamic Execute(string[] args)
        {
            var result = runtime.Invoke(args);

            if (args[0] == "wizz") result = "";

            /**
             * BROADCAST TO ALL CLIENTS
             */
            Clients.All.Callback(args, result);

            return result;
        }
    }
}