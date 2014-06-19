
namespace Service.Models
{
    public partial class Message : Utils.BaseEntity<int>
    {
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public string Content { get; set; }
    }
}
