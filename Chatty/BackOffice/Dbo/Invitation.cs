using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackOffice.Dbo
{
    public partial class Invitation : Utils.BaseEntity<int>
    {
        [Index("IX_Invitation", 1, IsUnique = true)]
        public int FromUserId { get; set; }
        [Index("IX_Invitation", 2, IsUnique = true)]
        public int ToUserId { get; set; }
        [StringLength(255)]
        public string Content { get; set; }
    }
}