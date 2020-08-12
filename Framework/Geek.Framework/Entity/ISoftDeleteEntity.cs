using System;
namespace Geek.Framework.Entity
{
    /// <summary>
    /// 实体软删除接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ISoftDeleteEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        int IsDelete { get; set; }
    }

    public interface ISoftDeleteEntity : ISoftDeleteEntity<long>
    {

    }
}
