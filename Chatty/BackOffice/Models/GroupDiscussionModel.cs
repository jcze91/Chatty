using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Models
{
    public class GroupDiscussionModel : DiscussionModel
    {
        [JsonProperty("groupid")]
        public int GroupId { get; set; }
        [JsonProperty("usertoid")]
        public int UserToId { get; set; }
        [JsonProperty("groupname")]
        public string GroupName { get; set; }
    }
}