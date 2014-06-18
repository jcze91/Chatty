﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Invitation : Utils.BaseModel<int>
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; }
    }
}
