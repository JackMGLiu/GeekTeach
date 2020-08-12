using System;
namespace Geek.Framework.Entity
{
    public abstract class FullAuditEntity<TKey> : AuditEntity<TKey>, IFullAuditEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey CreateBy { get; set; }
        public virtual TKey ModiflyBy { get; set; }
    }

    public abstract class FullAuditEntity : AuditEntity<long>, IFullAuditEntity
    {
        public virtual long CreateBy { get; set; }
        public virtual long ModiflyBy { get; set; }
    }
}
