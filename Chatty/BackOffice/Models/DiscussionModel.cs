using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Models
{
    public class DiscussionModel
    {
        [JsonProperty("userfromid")]
        public int UserFromId { get; set; }
        [JsonProperty("users")]
        public IList<UserModel> Users { get; set; }
        [JsonProperty("messages")]
        public IList<MessageModel> Messages { get; set; }
    }
}