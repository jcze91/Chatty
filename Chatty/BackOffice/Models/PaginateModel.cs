using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Chatty.BackOffice.Models
{
    public class PaginateModel<T>
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }

        [JsonProperty("nextPage")]
        public int? NextPage { get; set; }

        [JsonProperty("prevPage")]
        public int? PrevPage { get; set; }

        [JsonProperty("items")]
        public IList<T> Items { get; set; }
    }
}