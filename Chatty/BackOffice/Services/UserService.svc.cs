
using System;
using WebMatrix.WebData;
namespace BackOffice.Services
{
    public class UserService : Utils.BaseService<int, Dbo.User, DataAccess.UserDao>, Contracts.UserContract 
    {
    }
}
