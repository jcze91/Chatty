using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Dbo
{
    public partial class Contact : Utils.BaseEntity<int>
    {
        public int UserId { get; set; }
        public int ContactId { get; set; }
    }
}
