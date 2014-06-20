
namespace BackOffice.Providers
{
    public class DepartmentProvider : Utils.BaseProvider<int, Dbo.Department, DataAccess.DepartmentDao, Services.DepartmentService>
    {
        protected override string getClass() { return "department"; }

        protected override int GetFieldCount() { return 2; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Department()
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
                return service.Update(new Dbo.Department()
                {
                    Id = int.Parse(args[1]),
                    Name = args[2]
                });
            }
            catch { return null; }
        }
    }
}