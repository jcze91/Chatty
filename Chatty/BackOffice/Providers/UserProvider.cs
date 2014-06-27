using BackOffice.Utils;

namespace BackOffice.Providers
{
    public class UserProvider : Utils.BaseProvider<int, Dbo.User, DataAccess.UserDao, Services.UserService>
    {
        protected override string getClass() { return "user"; }

        protected override int GetFieldCount() { return 9; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.User()
                {
                    Username = args[1],
                    Lastname = args[2],
                    Firstname = args[3],
                    Email = args[4],
                    Password = args[5],
                    isEnable = bool.Parse(args[6]),
                    Thumbnail = args[7],
                    DepartmentId = int.Parse(args[8])
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.User()
                {
                    Id = int.Parse(args[1]),
                    Username = args[2],
                    Lastname = args[3],
                    Firstname = args[4],
                    Email = args[5],
                    Password = args[6],
                    isEnable = bool.Parse(args[7]),
                    Thumbnail = args[8],
                    DepartmentId = int.Parse(args[9])
                });
            }
            catch { return null; }
        }
    }
}