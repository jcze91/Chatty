using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackOffice.Dbo
{
    public partial class Contact : Utils.BaseEntity<int>
    {
        [Index("IX_UserContact", 1, IsUnique = true)]
        public int UserId { get; set; }
        [Index("IX_UserContact", 2, IsUnique = true)]
        public int ContactId { get; set; }
    }
}