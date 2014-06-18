using System.ServiceModel;

namespace Service.Contracts
{

    [ServiceContract]
    public interface GroupContract : IRepository<int, Models.Group>
    {
    }
}
