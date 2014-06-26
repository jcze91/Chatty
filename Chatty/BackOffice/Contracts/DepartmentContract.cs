using BackOffice.Models;
using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface DepartmentContract : IRepository<int, Dbo.Department>  
    {
        [OperationContract]
        PaginateModel<DepartmentModel> GetFilteredDepartments(string adminId, string token, string page, string pageSize,
                                    string order, string filter = null);
    }
}