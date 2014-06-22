using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Providers
{
    public class MessageProvider : Utils.BaseProvider<int, Dbo.Message, DataAccess.MessageDao, Services.MessageService>
    {
        protected override string getClass() { return "message"; }

        protected override int GetFieldCount() { return 4; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Message()
                {
                    UserFromId = int.Parse(args[1]),
                    UserToId = int.Parse(args[2]),
                    Content = args[3]
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.Message()
                {
                    Id = int.Parse(args[1]),
                    UserFromId = int.Parse(args[2]),
                    UserToId = int.Parse(args[3]),
                    Content = args[4]
                });
            }
            catch { return null; }
        }
    }
}