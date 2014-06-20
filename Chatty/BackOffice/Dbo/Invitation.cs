using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Dbo
{
    public partial class Invitation : Utils.BaseEntity<int>
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; }
    }
}