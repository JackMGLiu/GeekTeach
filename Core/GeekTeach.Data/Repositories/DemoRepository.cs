using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories;
using GeekTeach.Domain;

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
    }

}
