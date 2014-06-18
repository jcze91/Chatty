
namespace Service.Services
{
    public class MessageService : Utils.BaseService<long, Models.Message, DataAccess.MessageDao>, Contracts.MessageContract
    {

    }
}
