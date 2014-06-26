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
            if (admin.Token != token)
                return null;

            IList<UserModel> usersList = this.dao.GetFilteredUsers(ipage, ipageSize, iorder, filter)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    isEnable = u.isEnable,
                    Lastname = u.Lastname,
                    Firstname = u.Firstname,
                    Username = u.Username,
                    Email = u.Email
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
    }
}
