
using BackOffice.Models;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;
namespace BackOffice.Services
{
    public class MessageService : Utils.BaseService<int, Dbo.Message, DataAccess.MessageDao>, Contracts.MessageContract {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }
        
        [WebGet(UriTemplate = "GetSimpleDiscussion/{adminId}/{token}/{user1id}/{user2id}",
         ResponseFormat = WebMessageFormat.Json)]
        public SimpleDiscussionModel GetSimpleDiscussion(string adminId, string token, string user1id, string user2id)
        {
            int iuser1id = -1;
            int.TryParse(user1id, out iuser1id);
            int iuser2id = -1;
            int.TryParse(user2id, out iuser2id);
            int iadminId = -1;
            int.TryParse(adminId, out iadminId);

            var admin = userService.GetById(iadminId);
            if (admin.Token != token)
                return null;

            List<UserModel> users = new List<UserModel>();
            users.Add(new UserModel { Username = userService.GetById(iuser1id).Username });
            users.Add(new UserModel { Username = userService.GetById(iuser2id).Username });
            var messages = this.SearchFor(m => (m.UserFromId == iuser1id && m.UserToId == iuser2id)
                || (m.UserFromId == iuser2id && m.UserToId == iuser1id)).Select(m =>
                new MessageModel { Id = m.Id, Content = m.Content, UserName = userService.GetById(m.UserFromId).Username, Date = m.CreatedAt.ToString() })
                .ToList();
            return new SimpleDiscussionModel
            {
                UserFromId = iuser1id,
                UserToId = iuser2id,
                Users = users,
                Messages = messages
            };
        }
        [WebGet(UriTemplate = "GetSimpleDiscussions/{adminId}/{token}/{page}/{pageSize}",
          ResponseFormat = WebMessageFormat.Json)]
        public PaginateModel<SimpleDiscussionModel> GetSimpleDiscussions(string adminId, string token, string page, string pageSize)
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

            var convs = this.dao.GetDiscussions(ipage, ipageSize);
            IList<SimpleDiscussionModel> list = new List<SimpleDiscussionModel>();
            if (convs != null)
            {
                foreach (var conv in convs)
                {
                    List<UserModel> users = new List<UserModel>();
                    users.Add(new UserModel { Username = userService.GetById(conv.UserFromId).Username });
                    users.Add(new UserModel { Username = userService.GetById(conv.UserToId).Username });

                    list.Add(new SimpleDiscussionModel
                        {
                            UserFromId = conv.UserFromId,
                            UserToId = conv.UserToId,
                            Users = users
                        });
                }
            }

            var totalCount = this.dao.GetDiscussionsCount();
            return new PaginateModel<SimpleDiscussionModel>
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
