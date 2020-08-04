using System;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    public class Db : IDisposable
    {
        internal static ILogger<Db> Logger { get; set; }

        public IDbConnection Connection { get; private set; }

        public Db(IDbConnection connection)
        {
            this.Connection = connection;
        }

        #region 事务

        private IDbTransaction _transaction;

        /// <summary>
        /// 事务属性
        /// </summary>
        public IDbTransaction Transaction
        {
            get => _transaction == null || _transaction.Connection == null ? null : _transaction;
            private set => _transaction = value;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Unspecified);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            Transaction = Connection.BeginTransaction(il);
            return Transaction;
        }

        #endregion

        public void Dispose()
        {
            Transaction?.Dispose();
            Transaction = null;
            Connection?.Dispose();
            Connection = null;
        }
    }
}
