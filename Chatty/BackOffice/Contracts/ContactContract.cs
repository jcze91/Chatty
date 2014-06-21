using System.ServiceModel;

namespace BackOffice.Contracts
{
    [ServiceContract]
    public interface ContactContract : IRepository<int, Dbo.Contact> { }
}