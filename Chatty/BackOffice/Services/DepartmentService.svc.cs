
using BackOffice.Models;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Linq;
namespace BackOffice.Services
{
    public class DepartmentService : Utils.BaseService<int, Dbo.Department, DataAccess.DepartmentDao>, Contracts.DepartmentContract
    {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }

        [WebGet(UriTemplate = "AddDepartment/{adminId}/{token}/{departmentName}",
           ResponseFormat = WebMessageFormat.Json)]
        public string AddDepartment(string adminId, string token, string departmentName)
        {
            int iadminId = -1;
            int.TryParse(adminId, out iadminId);

            var admin = userService.GetById(iadminId);
            if (admin.Token != token)
                return null;

            var departmentExists = this.SearchFor(d => d.Name == departmentName).Any();
            if (departmentExists)
                return "ERROR";
            Dbo.Department newDpt = new Dbo.Department();
            newDpt.Name = departmentName;
            this.Insert(newDpt);

            return "SUCCESS";
        }

        [WebGet(UriTemplate = "GetDepartment/{adminId}/{token}/{departmentId}",
            ResponseFormat = WebMessageFormat.Json)]
        public DepartmentModel GetDepartment(string adminId, string token, string departmentId)
        {
            int idepartmentId = -1;
            int.TryParse(departmentId, out idepartmentId);
            int iadminId = -1;
            int.TryParse(adminId, out iadminId);

            var admin = userService.GetById(iadminId);
            if (admin.Token != token)
                return null;

            var department = this.GetById(idepartmentId);
            var users = userService.SearchFor(u => u.DepartmentId == department.Id).Select(user => new UserModel {
                    Id = user.Id,
                    isEnable = user.isEnable,
                    Lastname = user.Lastname,
                    Firstname = user.Firstname,
                    Username = user.Username,
                    Email = user.Email,
                    ConnexionDate = user.ConnexionDate.ToString(),
                    Job = user.Job
            }).ToList();
            return new DepartmentModel
            {
                Id = department.Id,
                Name = department.Name,
                Users = users,
                UsersCount = users.Count
            };
        }
        [WebGet(UriTemplate = "GetFilteredDepartments/{adminId}/{token}/{page}/{pageSize}/{order}?filter={filter}",
           ResponseFormat = WebMessageFormat.Json)]
        public PaginateModel<DepartmentModel> GetFilteredDepartments(string adminId, string token, string page, string pageSize, string order, string filter = null)
        {
            int id = -1;
            int ipage = -1;
            int ipageSize = -1;
            int iorder = -1;
            int.TryParse(adminId, out id);
            int.TryParse(page, out ipage);
            int.TryParse(pageSize, out ipageSize);
            int.TryParse(order, out iorder);

            var admin = userService.GetById(id);
            if (admin.Token != token)
                return null;

            IList<DepartmentModel> list = this.dao.GetFilteredDepartments(ipage, ipageSize, iorder, filter)
                .Select(d => new DepartmentModel
                {
                   Id = d.Id,
                   Name = d.Name,
                   Users = null,
                   UsersCount = 0
                   
                }).ToList();
            var totalCount = this.dao.GetFilteredDepartmentsCount(ipage, ipageSize, iorder, filter);
            return new PaginateModel<DepartmentModel>
            {
                Items = list,
                TotalCount = totalCount,
                PageSize = ipageSize,
                NextPage = ipage + 1,
                PrevPage = ipage - 1,
            };

        }
   
    }
}
