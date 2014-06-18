using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Department : Utils.BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
