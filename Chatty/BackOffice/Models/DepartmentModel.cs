using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Models
{
    public class DepartmentModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("usersCount")]
        public int UsersCount { get; set; }
        [JsonProperty("users")]
        public IList<UserModel> Users { get; set; }
    }
}