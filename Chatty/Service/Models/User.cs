
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Service.Models
{
    public partial class User : Service.Utils.BaseEntity<int>
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string Password { get; set; }
        public bool isEnable { get; set; }

    }
}
