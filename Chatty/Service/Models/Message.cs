
namespace Service.Models
{
    public class Message : Utils.BaseEntity<long>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
