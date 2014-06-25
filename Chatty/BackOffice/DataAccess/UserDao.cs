using BackOffice.Dbo;
using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using System.Linq;

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
        public IList<User> GetFilteredUsers(int page, int pageSize, int order, string filter)
        {
            var query = this.ctx.Users.Where(u => !u.isAdmin);
            if (filter != null && filter != "")
                query = query.Where(u => u.Username.Contains(filter)
                    || u.Lastname.Contains(filter) || u.Firstname.Contains(filter));

            if (order == 0)
                query = query.OrderByDescending(x => x.Id);
            else if (order == 1)
                query = query.OrderBy(x => x.Username);
            else if (order == 2)
                query = query.OrderByDescending(x => x.Username);

            return query.Skip(pageSize * page).Take(pageSize).ToList();
        }
        public int GetFilteredUsersCount(int page, int pageSize, int order, string filter)
        {
            var query = this.ctx.Users.Where(u => !u.isAdmin);
            if (filter != null && filter != "")
                query = query.Where(u => u.Username.Contains(filter)
                    || u.Lastname.Contains(filter) || u.Firstname.Contains(filter));

            if (order == 0)
                query = query.OrderByDescending(x => x.Id);
            else if (order == 1)
                query = query.OrderBy(x => x.Username);
            else if (order == 2)
                query = query.OrderByDescending(x => x.Username);

            return query.Count();
        }
    }
}