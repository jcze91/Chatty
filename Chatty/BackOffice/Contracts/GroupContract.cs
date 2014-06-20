using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface GroupContract : IRepository<int, Dbo.Group>
    {
    }
}