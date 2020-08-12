using System;
namespace Geek.Framework.Entity
{
    /// <summary>
    /// 实体状态接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IStatusEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 启用代码
        /// </summary>
        int StatusCode { get; set; }
    }

    public interface IStatusEntity : IStatusEntity<long>
    {

    }
}
