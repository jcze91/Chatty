
using System;
using WebMatrix.WebData;
namespace BackOffice.DataAccess
{
    public class UserDao : Utils.BaseDao<int, Dbo.User>
    {
        public override Dbo.User Insert(Dbo.User entity)
        {
            Dbo.User e = null;
            if (!entity.isAdmin)
                e = base.Insert(entity);
            else
            {
                var password = entity.Password;
                entity.Password = null;
                e = base.Insert(entity);
                WebSecurity.CreateAccount(e.Username, password);
            }
            return e;
        }
    }
}