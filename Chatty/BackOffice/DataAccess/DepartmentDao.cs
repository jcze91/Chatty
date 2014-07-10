
using BackOffice.Dbo;
using System.Collections.Generic;
using System.Linq;
namespace BackOffice.DataAccess
{
    public class DepartmentDao : Utils.BaseDao<int, Dbo.Department> {
        public IList<Department> GetFilteredDepartments(int page, int pageSize, int order, string filter)
        {
            var query = this.ctx.Departments.Where(r => true);

            if (filter != null && filter != "")
                query = query.Where(u => u.Name.Contains(filter));

            if (order == 0)
                query = query.OrderByDescending(x => x.Id);
            else if (order == 1)
                query = query.OrderBy(x => x.Name);
            else if (order == 2)
                query = query.OrderByDescending(x => x.Name);

            var result = query.Skip(pageSize * page).Take(pageSize).ToList();
            return result;
        }
        public int GetFilteredDepartmentsCount(int page, int pageSize, int order, string filter)
        {
            var query = this.ctx.Departments.Where(r => true);

            if (filter != null && filter != "")
                query = query.Where(u => u.Name.Contains(filter));

            if (order == 0)
                query = query.OrderByDescending(x => x.Id);
            else if (order == 1)
                query = query.OrderBy(x => x.Name);
            else if (order == 2)
                query = query.OrderByDescending(x => x.Name);

            int result = query.Count();
            return result;
        }
    }
}