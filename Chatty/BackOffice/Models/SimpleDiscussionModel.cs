using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Models
{
    public class SimpleDiscussionModel : DiscussionModel
    {
        [JsonProperty("usertoid")]
        public int UserToId { get; set; }
    }
}