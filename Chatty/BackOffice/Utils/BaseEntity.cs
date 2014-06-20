using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackOffice.Utils
{
    /// <summary>
    /// BaseModel for mapping
    /// </summary>
    /// <typeparam name="K">table id key type</typeparam>
    public abstract class BaseEntity<K>
        where K : struct, IEquatable<K>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public K Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}