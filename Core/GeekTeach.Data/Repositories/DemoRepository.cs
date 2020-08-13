using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories;
using GeekTeach.Domain;
using Dapper;

namespace GeekTeach.Data.Repositories
{
    public class DemoRepository : Repository<Demo>, IDemoRepository
    {
        public DemoRepository(Db db) : base(db)
        {

        }

        public Task<IEnumerable<Demo>> DemoList()
        {
            //var param = new { Age = 22 };
            //var sql = SqlBuilder.Select("Demo", param);
            return this.QueryAsync(null);
        }

        public Task<IEnumerable<Demo>> DemoListByWhere(int obj)
        {
            Dapper.SqlBuilder builder = new Dapper.SqlBuilder();
            var query = $@"select * from {typeof(Demo).Name} /**where**/ /**orderby**/ ";
            var template = builder.AddTemplate(query);
            builder.Where("Age=@a", new { a = obj });
            builder.OrderBy("Id");
            using (Db.Connection)
            {
                var data = this.Db.Connection.QueryAsync<Demo>(template.RawSql, template.Parameters);
                return data;
            }
        }
    }

}
