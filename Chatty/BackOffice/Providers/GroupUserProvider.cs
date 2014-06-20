
namespace BackOffice.Providers
{
    public class GroupUserProvider : Utils.BaseProvider<int, Dbo.GroupUser, DataAccess.GroupUserDao, Services.GroupUserService>
    {
        protected override string getClass() { return "groupuser"; }

        protected override int GetFieldCount() { return 3; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.GroupUser()
                {
                    GroupId = int.Parse(args[1]),
                    UserId= int.Parse(args[2])
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.GroupUser()
                {
                    Id = int.Parse(args[1]),
                    GroupId = int.Parse(args[2]),
                    UserId = int.Parse(args[3])
                });
            }
            catch { return null; }
        }
    }
}