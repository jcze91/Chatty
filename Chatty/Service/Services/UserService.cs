using System.Linq;

namespace Service.Services
{
    public class UserService : Utils.BaseService<int, Models.User, DataAccess.UserDao>, Contracts.UserContract
    {

        public int Login(string username, string password)
        {
            var res = SearchFor(x => x.Username == username && x.Password == password);
            if (res.Count() == 1)
                return res.First().Id;
            else
                return -1;
        }
    }
}
