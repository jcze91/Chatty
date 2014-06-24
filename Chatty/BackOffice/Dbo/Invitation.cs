using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackOffice.Dbo
{
    public partial class Invitation : Utils.BaseEntity<int>
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        [StringLength(255)]
        public string Content { get; set; }
    }
}