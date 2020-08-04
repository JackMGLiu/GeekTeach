using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> FindAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TEntity>> QueryAsync(object clause)
        {
            var sql = SqlBuilder.Select(TableName, clause);
            return Db.QueryAsync<TEntity>(sql, clause);
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
