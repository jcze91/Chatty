
namespace Chatty.Dbo
{
    public class GroupUser : Utils.BaseModel<int>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
