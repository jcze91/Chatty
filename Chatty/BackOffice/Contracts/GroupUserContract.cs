using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface GroupUserContract : IRepository<int, Dbo.GroupUser>
    {
    }
}