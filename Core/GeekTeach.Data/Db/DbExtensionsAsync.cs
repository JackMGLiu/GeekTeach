using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// 数据库操作异步扩展方法
    /// </summary>
    public static class DbExtensionsAsync
    {
        // public static Task<IEnumerable<T>> QueryAsync<T>(this Db db, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
        //SqlMapper.QueryAsync<T>(db.Connection, new CommandDefinition(sql, param, db.Transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
    }
}
