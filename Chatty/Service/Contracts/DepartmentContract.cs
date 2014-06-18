using System.ServiceModel;

namespace Service.Contracts
{

    [ServiceContract]
    public interface DepartmentContract : IRepository<int, Models.Department>
    {
    }
}
