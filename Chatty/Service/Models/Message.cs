
namespace Service.Models
{
    public class Message : Utils.BaseModel<long>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
