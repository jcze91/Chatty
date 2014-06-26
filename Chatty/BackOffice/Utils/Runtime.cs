using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Utils
{
    public class Runtime
    {
        private readonly Dictionary<string, dynamic> commands;

        public Runtime()
        {
            commands = new Dictionary<string, dynamic>();
        }

        public void RegisterCommand(string command, dynamic provider)
        {
            commands[command] = provider;
        }

        public dynamic Invoke(string[] args)
        {
            try
            {
                if (args.Length != 0 && commands.ContainsKey(args.First()))
                    return commands[args.First()].Execute(args);
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    }
}