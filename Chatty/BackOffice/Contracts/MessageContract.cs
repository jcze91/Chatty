using BackOffice.Models;
using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface MessageContract : IRepository<int, Dbo.Message>
    {
        [OperationContract]
        SimpleDiscussionModel GetSimpleDiscussion(string adminId, string token, string user1id, string user2id);
        [OperationContract]
        PaginateModel<SimpleDiscussionModel> GetSimpleDiscussions(string adminId, string token, string page, string pageSize);
    }
}