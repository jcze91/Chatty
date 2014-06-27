
using BackOffice.Dbo;
using System.Collections.Generic;
using System.Linq;

namespace BackOffice.DataAccess
{
    class DistinctMessageComparer : IEqualityComparer<Message>
    {

        public bool Equals(Message x, Message y)
        {
            return (x.UserFromId == y.UserFromId && x.UserToId == y.UserToId)
                || (x.UserToId == y.UserFromId && x.UserFromId == y.UserToId);
        }

        public int GetHashCode(Message obj)
        {
            return obj.UserFromId.GetHashCode() ^ obj.UserToId.GetHashCode();
        }
    }
    public class MessageDao : Utils.BaseDao<int, Dbo.Message> 
    {
        public IList<Dbo.Message> GetDiscussions(int page, int pageSize)
        {
            var query = this.ctx.Messages.Where(r => true).AsEnumerable().Distinct(new DistinctMessageComparer());
            var ret = query.OrderByDescending(d => d.Id).Skip(pageSize * page).Take(pageSize).ToList();
            return ret;
        }
        public int GetDiscussionsCount()
        {
            var query = this.ctx.Messages.Where(r => true).AsEnumerable().Distinct(new DistinctMessageComparer());
            return query.Count();
        }
    }
}