using System;
using System.Collections.Generic;

namespace BackOffice.Models
{
    public class AdminModel
    {
        public UserModel ConnectedAdmin { get; set; }
        public string RestURL { get; set; }
    }
}