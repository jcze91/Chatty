using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface MessageContract : IRepository<int, Dbo.Message>
    {
    }
}