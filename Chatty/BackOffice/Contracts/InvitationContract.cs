using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface InvitationContract : IRepository<int, Dbo.Invitation> { }
}