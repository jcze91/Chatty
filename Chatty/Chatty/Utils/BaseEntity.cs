using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Utils
{
    /// <summary>
    /// BaseModel for mapping
    /// </summary>
    /// <typeparam name="K">table id key type</typeparam>
    public abstract class BaseEntity<K>
        where K : struct, IEquatable<K>
    {
        public K Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
