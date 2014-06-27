using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackOffice.Dbo
{
    public partial class User : Utils.BaseEntity<int>
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Username { get; set; }
        [StringLength(255)]
        public string Lastname { get; set; }
        [StringLength(255)]
        public string Firstname { get; set; }

        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [JsonIgnore]
        [StringLength(255)]
        public string Password { get; set; }
        [JsonIgnore]
        public bool isEnable { get; set; }
        [JsonIgnore]
        public bool isAdmin { get; set; }

        public int DepartmentId { get; set; }
        public string Thumbnail { get; set; }

        [StringLength(255)]
        public string Token { get; set; }
        public DateTime? ConnexionDate { get; set; }
        [StringLength(255)]
        public string Job { get; set; }

    }
}