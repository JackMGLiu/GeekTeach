namespace Geek.Framework.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<long>
    {
    }
}
