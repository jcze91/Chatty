
using System;
using WebMatrix.WebData;
namespace BackOffice.Services
{
    public class UserService : Utils.BaseService<int, Dbo.User, DataAccess.UserDao>, Contracts.UserContract 
    {
        public void CheckSuperAdminCreation()
        {
            WebSecurity.CreateUserAndAccount("admin", "admin", propertyValues: new
            {
                LastName = "Chatty",
                FirstName = "Master",
                Email = "master@chatty.net",
                IsEnable = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
        }
    }
}
