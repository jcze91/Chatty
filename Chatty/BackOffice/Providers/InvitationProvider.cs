
namespace BackOffice.Providers
{
    public class InvitationProvider : Utils.BaseProvider<int, Dbo.Invitation, DataAccess.InvitationDao, Services.InvitationService>
    {
        protected override string getClass() { return "invitation"; }

        protected override int GetFieldCount() { return 4; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Invitation()
                {
                    FromUserId = int.Parse(args[1]),
                    ToUserId = int.Parse(args[2]),
                    Content = args[3]
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.Invitation()
                {
                    Id = int.Parse(args[1]),
                    FromUserId = int.Parse(args[2]),
                    ToUserId = int.Parse(args[3]),
                    Content = args[4]
                });
            }
            catch { return null; }
        }
    }
}