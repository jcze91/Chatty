using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface UserContract : IRepository<int, Dbo.User>
    {
    }
}