using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Entity;
using GeekTeach.Data.Db;

namespace GeekTeach.Web.Models
{
    public class Demo : IEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }

    public interface IDemoRepository : IRepository<Demo>
    {
        Task<IEnumerable<Demo>> DemoList();
    }

    public class DemoRepository : Repository<Demo>, IDemoRepository
    {
        public DemoRepository(Db db) : base(db)
        {

        }

        public Task<IEnumerable<Demo>> DemoList()
        {
            var sql = SqlBuilder.Select("Demo", null);
            return Db.QueryAsync<Demo>(sql);
        }
    }




    public interface IDemoServive
    {
        Task<IEnumerable<Demo>> DemoList();
    }

    public class DemoServive : IDemoServive
    {
        private readonly IDemoRepository _repository;

        public DemoServive(IDemoRepository repository)
        {
            this._repository = repository;
        }

        public Task<IEnumerable<Demo>> DemoList()
        {
            return _repository.DemoList();
        }
    }
}
