using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Geek.Framework.Entity;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// 数据仓储基类接口实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Db Db { get; }

        public Repository(Db db)
        {
            this.Db = db;
        }

        public string TableName => typeof(TEntity).Name;

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Unspecified);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return Db.BeginTransaction(il);
        }

        public virtual Task<TEntity> FindAsync(object clause)
        {
            var sql = SqlBuilder.Select(TableName, clause);
            return Db.Connection.QuerySingleOrDefaultAsync<TEntity>(sql, clause);
        }

        public virtual Task<TEntity> FindAsync(TKey key)
        {
            return FindAsync(new { Id = key });
        }

        public virtual Task<IEnumerable<TEntity>> QueryAsync(object clause)
        {
            var sql = SqlBuilder.Select(TableName, clause);
            return Db.Connection.QueryAsync<TEntity>(sql, clause);
        }

        public virtual Task InsertAsync(TEntity entity, TKey userId = default)
        {
            //FillCreateAudit(entity, operatorId);
            var sql = SqlBuilder.Insert(TableName, entity);
            return Db.Connection.ExecuteAsync(sql, entity);
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities, TKey userId = default)
        {
            if (entities == null || entities.Count() == 0)
                return Task.CompletedTask;
            //foreach (var ent in entities)
            //    FillCreateAudit(ent, operatorId);
            var sql = SqlBuilder.Insert(TableName, typeof(TEntity));
            return Db.Connection.ExecuteAsync(sql, entities);
        }

        public virtual Task<int> UpdateAsync(object update, object clause)
        {
            var sql = SqlBuilder.Update(TableName, update, clause);
            return Db.Connection.ExecuteAsync(sql, SqlBuilder.MergeParams(update, clause));
        }

        public virtual Task<int> UpdateAsync(TEntity entity, TKey userId = default)
        {
            //FillUpdateAudit(entity, operatorId);
            var updateColumns = SqlBuilder.GetParamNames(entity).Where(x => x != "Id");
            var sql = SqlBuilder.Update(TableName, updateColumns, new { entity.Id });
            return Db.Connection.ExecuteAsync(sql, entity);
        }

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, TKey userId = default)
        {
            //foreach (var ent in entities)
            //    FillUpdateAudit(ent, operatorId);
            var updateColumns = SqlBuilder.GetParamNames(typeof(TEntity)).Where(x => x != "Id");
            var sql = SqlBuilder.Update(TableName, updateColumns, new { Id = default(long) });
            return Db.Connection.ExecuteAsync(sql, entities);
        }

        public virtual Task<int> DeleteAsync(object clause)
        {
            var sql = SqlBuilder.Delete(TableName, clause);
            return Db.Connection.ExecuteAsync(sql, clause);
        }

        public virtual Task<int> DeleteAsync(TKey id)
        {
            return DeleteAsync(new { Id = id });
        }
    }

    public class Repository<TEntity> : Repository<TEntity, long>
        where TEntity : IEntity<long>
    {
        public Repository(Db db) : base(db)
        {

        }
    }
}
