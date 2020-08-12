using System;
namespace Geek.Framework.Entity
{
    public abstract class AuditEntity<TKey> : EntityBase<TKey>, IAuditEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        public virtual DateTime ModiflyTime { get; set; } = DateTime.Now;
    }

    public abstract class AuditEntity : EntityBase<long>, IAuditEntity
    {
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        public virtual DateTime ModiflyTime { get; set; } = DateTime.Now;
    }
}
