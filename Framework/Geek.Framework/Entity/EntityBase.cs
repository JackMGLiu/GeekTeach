using System;
using Dapper.Contrib.Extensions;

namespace Geek.Framework.Entity
{
    public abstract class EntityBase<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<long>, IEntity
    {
    }
}
