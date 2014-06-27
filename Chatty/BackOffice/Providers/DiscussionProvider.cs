using Microsoft.Practices.Unity;

namespace BackOffice.Providers
{
    public class DiscussionProvider : Utils.BaseProvider<int, Dbo.Discussion, DataAccess.DiscussionDao, Services.DiscussionService>
    {
        protected override string getClass() { return "discussion"; }

        protected override int GetFieldCount() { return 4; }
        public DiscussionProvider()
            : base()
        {
            Startup.container.Resolve<Utils.Runtime>().RegisterCommand(cmd_getByGroupId, this);
        }

        private string cmd_getByGroupId { get { return getClass() + "-getByGroupId"; } }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Discussion()
                {
                    GroupId = int.Parse(args[1]),
                    UserFromId = int.Parse(args[2]),
                    Content = args[3]
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.Discussion()
                {
                    Id = int.Parse(args[1]),
                    GroupId = int.Parse(args[2]),
                    UserFromId = int.Parse(args[3]),
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
            * GET BY GROUP ID
            */
            if (args[0] == cmd_getByGroupId && args.Length == 2)
            {
                int groupId = int.Parse(args[1]);
                return service.SearchFor(x => x.GroupId == groupId);
            }

            return null;
        }
    }
}