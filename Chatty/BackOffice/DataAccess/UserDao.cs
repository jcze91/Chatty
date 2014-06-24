
using System;
using WebMatrix.WebData;
namespace BackOffice.DataAccess
{
    public class UserDao : Utils.BaseDao<int, Dbo.User>
    {
        public override Dbo.User Insert(Dbo.User entity)
        {
            var e = base.Insert(entity);
            WebSecurity.CreateAccount(e.Username, e.Password);
            return e;
        }
    }
}