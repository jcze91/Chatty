
using System.ComponentModel.DataAnnotations;
namespace BackOffice.Dbo
{
    public partial class Message : Utils.BaseEntity<int>
    {
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        [StringLength(1000)]
        public string Content { get; set; }
    }
}