using BackOffice.Models;
using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface DiscussionContract : IRepository<int, Dbo.Discussion>
    {
        [OperationContract]
        GroupDiscussionModel GetGroupDiscussion(string adminId, string token, string groupId);
        [OperationContract]
        PaginateModel<GroupDiscussionModel> GetGroupDiscussions(string adminId, string token, string page, string pageSize);
    }
}