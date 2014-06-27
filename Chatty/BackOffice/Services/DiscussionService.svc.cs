
using BackOffice.Models;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;
namespace BackOffice.Services
{
    public class DiscussionService : Utils.BaseService<int, Dbo.Discussion, DataAccess.DiscussionDao>, Contracts.DiscussionContract 
    {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }
        private MessageService messageService { get { return (MessageService)Startup.container.Resolve(typeof(MessageService), "MessageService"); } }
        private GroupService groupService { get { return (GroupService)Startup.container.Resolve(typeof(GroupService), "GroupService"); } }

        [WebGet(UriTemplate = "GetGroupDiscussion/{adminId}/{token}/{groupId}",
           ResponseFormat = WebMessageFormat.Json)]
        public GroupDiscussionModel GetGroupDiscussion(string adminId, string token, string groupId)
        {
            int igroupId = -1;
            int.TryParse(groupId, out igroupId);
            int iadminId = -1;
            int.TryParse(adminId, out iadminId);

            var admin = userService.GetById(iadminId);
            if (admin.Token != token)
                return null;

            var messages = this.SearchFor(m => m.GroupId == igroupId).Select(m =>
               new MessageModel { Id = m.Id, Content = m.Content, UserName = userService.GetById(m.UserFromId).Username, Date = m.CreatedAt.ToString() })
               .ToList();
            return new GroupDiscussionModel
            {  
                GroupId = igroupId,
                Messages = messages,
                GroupName = groupService.GetById(igroupId).Name
            };
        }

        [WebGet(UriTemplate = "GetGroupDiscussions/{adminId}/{token}/{page}/{pageSize}",
           ResponseFormat = WebMessageFormat.Json)]
        public PaginateModel<GroupDiscussionModel> GetGroupDiscussions(string adminId, string token, string page, string pageSize)
        {
            int id = -1;
            int ipage = -1;
            int ipageSize = -1;
            int.TryParse(adminId, out id);
            int.TryParse(page, out ipage);
            int.TryParse(pageSize, out ipageSize);

            var admin = userService.GetById(id);
            if (admin.Token != token)
                return null;

            IList<GroupDiscussionModel> list = this.dao.GetDiscussions(ipage, ipageSize)
                .Select(d => new GroupDiscussionModel
                {
                    GroupId = d.GroupId,
                    UserFromId = d.UserFromId,
                    Users = userService.GetUsersByUserFromIdAndGroupId(d.UserFromId, d.GroupId),
                    GroupName = groupService.GetById(d.GroupId).Name
                }).ToList();
            var totalCount = this.dao.GetDiscussionsCount();
            return new PaginateModel<GroupDiscussionModel>
            {
                Items = list,
                TotalCount = totalCount,
                PageSize = ipageSize,
                NextPage = ipage + 1,
                PrevPage = ipage - 1,
            };

        }
   
    }
}
