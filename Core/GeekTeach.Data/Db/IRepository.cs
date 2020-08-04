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

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="il">事务隔离级别</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel il);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="clause">条件参数</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> QueryAsync(object clause);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="clause">条件参数</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object clause);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(TKey key);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="userId">操作人Id</param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity, TKey userId = default);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <param name="userId">操作人Id</param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, TKey userId = default);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="update">更新参数</param>
        /// <param name="clause">条件参数</param>
        /// <returns></returns>
        Task<int> UpdateAsync(object update, object clause);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="userId">操作人Id</param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, TKey userId = default);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <param name="userId">操作人Id</param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, TKey userId = default);

        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="clause">条件参数</param>
        /// <returns></returns>
        Task<int> DeleteAsync(object clause);

        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="param">参数</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<PagedResult<TEntity>> GetPageList(PageInfo pageInfo, IDbTransaction transaction = null, int? commandTimeout = null);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long>
       where TEntity : IEntity<long>
    { }
}
