
using System.ComponentModel.DataAnnotations;
namespace BackOffice.Dbo
{
    public partial class Discussion : Utils.BaseEntity<int>
    {
        public int GroupId { get; set; }
        public int UserFromId { get; set; }
        [StringLength(1000)]
        public string Content { get; set; }
    }
}