using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Dbo
{
    public  class Department : Utils.BaseModel<int>
    {
        public string Name { get; set; }
    }
}
