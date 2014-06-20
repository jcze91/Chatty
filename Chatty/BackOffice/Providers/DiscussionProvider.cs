
namespace BackOffice.Providers
{
    public class DiscussionProvider : Utils.BaseProvider<int, Dbo.Discussion, DataAccess.DiscussionDao, Services.DiscussionService>
    {
        protected override string getClass() { return "discussion"; }

        protected override int GetFieldCount() { return 4; }

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
    }
}