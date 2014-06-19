using System.ServiceModel;

namespace Service.Contracts
{

    [ServiceContract]
    public interface MessageContract : IRepository<int, Models.Message>
    {
    }
}
