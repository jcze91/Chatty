
using System.ComponentModel.DataAnnotations;
namespace BackOffice.Dbo
{
    public partial class Department : Utils.BaseEntity<int>
    {
        [StringLength(255)]
        public string Name { get; set; }
    }
}