
using System.ComponentModel.DataAnnotations.Schema;
namespace BackOffice.Dbo
{
    public partial class GroupUser : Utils.BaseEntity<int>
    {
        [Index("IX_GroupUser", 1, IsUnique = true)]
        public int GroupId { get; set; }
        [Index("IX_GroupUser", 2, IsUnique = true)]
        public int UserId { get; set; }
    }
}