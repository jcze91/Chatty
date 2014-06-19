using System.Linq;

namespace Service.Services
{
    public class UserService : Utils.BaseService<int, Models.User, DataAccess.UserDao>, Contracts.UserContract
    {

        public bool Login(string username, string password)
        {
            return SearchFor(x => x.Username == username && x.Password == password).Count() == 1;
        }
    }
}
