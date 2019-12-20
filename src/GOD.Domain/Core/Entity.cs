using System;
namespace backend.src.GOD.Domain.Core
{
    public class Entity<TKey> : ITrackableEntity
    {
        public TKey Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
