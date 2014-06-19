using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public partial class Discussion : Utils.BaseEntity<int>
    {
        public int GroupId { get; set; }
        public int UserFromId { get; set; }
        public string Content { get; set; }
    }
}
