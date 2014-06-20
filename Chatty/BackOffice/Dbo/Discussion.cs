
namespace BackOffice.Dbo
{
    public partial class Discussion : Utils.BaseEntity<int>
    {
        public int GroupId { get; set; }
        public int UserFromId { get; set; }
        public string Content { get; set; }
    }
}