using System;
namespace Geek.Framework.Entity
{
    public abstract class SoftDeleteEntity<TKey> : EntityBase<TKey>, ISoftDeleteEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual int IsDelete { get; set; } = 0;
    }

    public abstract class SoftDeleteEntity : EntityBase<long>, ISoftDeleteEntity
    {
        public virtual int IsDelete { get; set; } = 0;
    }
}
