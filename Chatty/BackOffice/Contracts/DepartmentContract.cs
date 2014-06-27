using BackOffice.Models;
using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface DepartmentContract : IRepository<int, Dbo.Department>  
    {
        [OperationContract]
        string AddDepartment(string adminId, string token, string departmentName);
        [OperationContract]
        DepartmentModel GetDepartment(string adminId, string token, string departmentId);
        [OperationContract]
        PaginateModel<DepartmentModel> GetFilteredDepartments(string adminId, string token, string page, string pageSize,
                                    string order, string filter = null);
    }
}