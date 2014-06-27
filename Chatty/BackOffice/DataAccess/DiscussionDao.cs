
using BackOffice.Dbo;
using System.Collections.Generic;
using System.Linq;
namespace BackOffice.DataAccess
{
    class DistinctDiscussionComparer : IEqualityComparer<Discussion>
    {

        public bool Equals(Discussion x, Discussion y)
        {
            return x.GroupId == y.GroupId && x.UserFromId == y.UserFromId;
        }

        public int GetHashCode(Discussion obj)
        {
            return obj.GroupId.GetHashCode() ^ obj.UserFromId.GetHashCode();
        }
    }
    public class DiscussionDao : Utils.BaseDao<int, Dbo.Discussion> 
    {
        public IList<Discussion> GetDiscussions(int page, int pageSize)
        {
            var query = this.ctx.Discussions.Where(d => true).AsEnumerable().Distinct(new DistinctDiscussionComparer());
            var ret = query.OrderByDescending(d => d.Id).Skip(pageSize * page).Take(pageSize).ToList();
            return ret;
        }
        public int GetDiscussionsCount()
        {
            var query = this.ctx.Discussions.Where(d => true).AsEnumerable().Distinct(new DistinctDiscussionComparer());
            return query.Count();
        }
    }
}