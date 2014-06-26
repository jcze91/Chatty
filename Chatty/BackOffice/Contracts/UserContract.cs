using BackOffice.Models;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface UserContract : IRepository<int, Dbo.User> 
    {
        [OperationContract]
        PaginateModel<UserModel> GetFilteredUsers(string adminId, string token, string page, string pageSize,
                                    string order, string filter = null);
    }
}