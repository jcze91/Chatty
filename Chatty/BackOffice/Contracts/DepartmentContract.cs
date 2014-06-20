using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface DepartmentContract : IRepository<int, Dbo.Department>
    {
    }
}