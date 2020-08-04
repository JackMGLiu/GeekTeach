using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Geek.Framework.Entity;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// 数据仓储基类接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        Db Db { get; }

        /// <summary>
        /// 表格名称
        /// </summary>
        string TableName { get; }

        IDbTransaction BeginTransaction();

        IDbTransaction BeginTransaction(IsolationLevel il);

        Task<IEnumerable<TEntity>> QueryAsync(object clause);

        Task<TEntity> FindAsync(object clause);

        Task<TEntity> FindAsync(TKey key);
    }


    public interface IRepository<TEntity> : IRepository<TEntity, long>
       where TEntity : IEntity<long>
    { }

}
