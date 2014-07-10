using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Linq;
using System.ServiceModel.Web;
using BackOffice.Models;

namespace BackOffice.Services
{
    public class UserService : Utils.BaseService<int, Dbo.User, DataAccess.UserDao>, Contracts.UserContract
    {
        private GroupUserService groupUserService { get { return (GroupUserService)Startup.container.Resolve(typeof(GroupUserService), "GroupUserService"); } }

        [WebGet(UriTemplate = "EditUser/{adminId}/{token}/{userId}/{userEmail}/{userFirstName}/{userLastName}/{job}/{departmentId}/{isBanned}",
             ResponseFormat = WebMessageFormat.Json)]
        public UserModel EditUser(string adminId, string token, string userId, string userEmail, string userFirstName, string userLastName, string job,
                string departmentId, string isBanned)
        {
            int iadminId = -1;
            int iuserId = -1;
            int idepartmentId = -1;

            bool bisBanned = false;
            int.TryParse(adminId, out iadminId);
            int.TryParse(userId, out iuserId);
            int.TryParse(departmentId, out idepartmentId);

            bool.TryParse(isBanned, out bisBanned);

            var admin = this.GetById(iadminId);
            if (admin.Token != token)
                return null;

            var user = this.dao.GetById(iuserId);
            user.Email = userEmail;
            user.Firstname = userFirstName;
            user.Lastname = userLastName;
            user.isEnable = !bisBanned;
            user.Job = job;
            user.DepartmentId = idepartmentId;
            this.dao.Update(user);
            return new UserModel
                {
                    Id = user.Id,
                    isEnable = user.isEnable,
                    Lastname = user.Lastname,
                    Firstname = user.Firstname,
                    Username = user.Username,
                    Email = user.Email,
                    ConnexionDate = user.ConnexionDate.ToString(),
                    Job = user.Job,
                    DepartmentId = user.DepartmentId
                };
        }
        [WebGet(UriTemplate = "GetFilteredUsers/{adminId}/{token}/{page}/{pageSize}/{order}?filter={filter}",
             ResponseFormat = WebMessageFormat.Json)]
        public PaginateModel<UserModel> GetFilteredUsers(string adminId, string token, string page, string pageSize, string order, string filter = null)
        {
            int id = -1;
            int ipage = -1;
            int ipageSize = -1;
            int iorder = -1;
            int.TryParse(adminId, out id);
            int.TryParse(page, out ipage);
            int.TryParse(pageSize, out ipageSize);
            int.TryParse(order, out iorder);

            var admin = this.GetById(id);
            if (admin == null || admin.Token != token)
                return null;

            IList<UserModel> usersList = this.dao.GetFilteredUsers(ipage, ipageSize, iorder, filter)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    isEnable = u.isEnable,
                    Lastname = u.Lastname,
                    Firstname = u.Firstname,
                    Username = u.Username,
                    Email = u.Email,
                    ConnexionDate = u.ConnexionDate.ToString(),
                    Job = u.Job,
                    DepartmentId = u.DepartmentId
                }).ToList();
            var totalCount = this.dao.GetFilteredUsersCount(ipage, ipageSize, iorder, filter);
            return new PaginateModel<UserModel>
            {
                Items = usersList,
                TotalCount = totalCount,
                PageSize = ipageSize,
                NextPage = ipage + 1,
                PrevPage = ipage - 1,
            };

        }

        public IList<UserModel> GetUsersByUserFromIdAndGroupId(int userId, int groupId)
        {
            HashSet<Dbo.User> map = new HashSet<Dbo.User>();
            Dbo.User user = this.GetById(userId);
            map.Add(user);
            var usersFromGroup = groupUserService.SearchFor(gu => gu.GroupId == groupId)
                    .Select(gu => this.GetById(gu.UserId)).ToList();
            foreach (var u in usersFromGroup)
                map.Add(u);

            return map.Select(u => new UserModel
                {
                    Id = u.Id,
                    isEnable = u.isEnable,
                    Lastname = u.Lastname,
                    Firstname = u.Firstname,
                    Username = u.Username,
                    Email = u.Email,
                    ConnexionDate = u.ConnexionDate.ToString(),
                    Job = u.Job,
                    DepartmentId = u.DepartmentId
                }).ToList();
        }
    }
}
