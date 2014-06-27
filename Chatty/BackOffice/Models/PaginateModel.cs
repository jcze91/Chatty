using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BackOffice.Models
{
    public class PaginateModel<T>
    {
        public int TotalCount { get; set; }
        public int? PageSize { get; set; }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public IList<T> Items { get; set; }
    }
}