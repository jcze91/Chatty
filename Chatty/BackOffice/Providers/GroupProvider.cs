
namespace BackOffice.Providers
{
    public class GroupProvider : Utils.BaseProvider<int, Dbo.Group, DataAccess.GroupDao, Services.GroupService>
    {
        protected override string getClass() { return "group"; }

        protected override int GetFieldCount() { return 2; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Group()
                {
                    Name = args[1]
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.Group()
                {
                    Id = int.Parse(args[1]),
                    Name = args[2]
                });
            }
            catch { return null; }
        }
    }
}