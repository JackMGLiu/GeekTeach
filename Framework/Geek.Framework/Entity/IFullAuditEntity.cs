using System;
namespace Geek.Framework.Entity
{
    public interface IFullAuditEntity<TKey> : IAuditEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey CreateBy { get; set; }
        TKey ModiflyBy { get; set; }
    }

    public interface IFullAuditEntity : IFullAuditEntity<long>, IAuditEntity
    {

    }
}
