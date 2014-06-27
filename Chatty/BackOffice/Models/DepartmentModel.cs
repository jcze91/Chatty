using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UsersCount { get; set; }
        public IList<UserModel> Users { get; set; }
    }
}