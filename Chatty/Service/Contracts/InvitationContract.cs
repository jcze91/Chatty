using System.ServiceModel;

namespace Service.Contracts
{

    [ServiceContract]
    public interface InvitationContract : IRepository<int, Models.Invitation>
    {
    }
}
