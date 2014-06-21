using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface DiscussionContract : IRepository<int, Dbo.Discussion> { }
}