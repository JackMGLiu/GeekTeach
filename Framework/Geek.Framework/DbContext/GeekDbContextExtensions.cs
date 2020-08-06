using System;
using System.Data;
using Geek.Framework;
using Geek.Framework.Db;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库上下文扩展
    /// </summary>
    public static class GeekDbContextExtensions
    {
        public static GeekBuilder AddDb<TDb, TDbConnection>(this GeekBuilder builder, string connectionString)
            where TDb : Db
            where TDbConnection : IDbConnection, new()
        {
            TDb implementationFactory(IServiceProvider serviceProvider)
            {
                if (Db.Logger == null)
                {
                    Db.Logger = serviceProvider.GetService<ILogger<Db>>();
                }

                var connection = new TDbConnection() as IDbConnection;
                connection.ConnectionString = connectionString;

                var db = (TDb)Activator.CreateInstance(typeof(TDb), connection);
                return db;
            }

            builder.Services.AddSingleton(implementationFactory);

            if (typeof(TDb) != typeof(Db))
            {
                builder.Services.AddScoped<Db>(implementationFactory);
            }

            return builder;
        }

        public static GeekBuilder AddDb<TDbConnection>(this GeekBuilder builder, string connectionString)
            where TDbConnection : IDbConnection, new()
        {
            return builder.AddDb<Db, TDbConnection>(connectionString);
        }
    }
}
