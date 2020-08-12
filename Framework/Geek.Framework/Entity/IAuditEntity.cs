using System;
namespace Geek.Framework.Entity
{
    /// <summary>
    /// 审计接口
    /// </summary>
    public interface IAuditEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 新增时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        DateTime ModiflyTime { get; set; }
    }

    public interface IAuditEntity : IAuditEntity<long>
    {

    }
}
