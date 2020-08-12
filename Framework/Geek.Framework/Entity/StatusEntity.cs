using System;
namespace Geek.Framework.Entity
{
    public abstract class StatusEntity<TKey> : EntityBase<TKey>, IStatusEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual int StatusCode { get; set; } = 0;
    }

    public abstract class StatusEntity : EntityBase<long>, IStatusEntity
    {
        public virtual int StatusCode { get; set; } = 0;
    }
}
