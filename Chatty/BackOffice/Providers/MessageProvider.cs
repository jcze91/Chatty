using BackOffice.Utils;
using Microsoft.Practices.Unity;

namespace BackOffice.Providers
{
    public class MessageProvider : Utils.BaseProvider<int, Dbo.Message, DataAccess.MessageDao, Services.MessageService>
    {
        protected override string getClass() { return "message"; }

        protected override int GetFieldCount() { return 4; }

        public MessageProvider()
            : base()
        {
            Startup.container.Resolve<Runtime>().RegisterCommand(cmd_getByContact, this);
        }

        private string cmd_getByContact { get { return getClass() + "-getByContact"; } }

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

        public override dynamic Execute(string[] args)
        {
            var res = base.Execute(args);
            if (res != null)
                return res; 
             
            /**
            * GET BY CONTACT
            */
            if (args[0] == cmd_getByContact && args.Length == 3)
            {
                int userId = int.Parse(args[1]);
                int contactId = int.Parse(args[2]);
                return service.SearchFor(x => x.UserFromId == userId && x.UserToId == contactId
                    || x.UserFromId == contactId && x.UserToId == userId);
            }

            return null;
        }
    }
}