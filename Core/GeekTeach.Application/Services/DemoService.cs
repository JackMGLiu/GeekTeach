using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories;
using GeekTeach.Domain;

namespace GeekTeach.Application.Services
{
    public class DemoService : IDemoService
    {
        private readonly IDemoRepository _repository;

        public DemoService(IDemoRepository repository)
        {
            this._repository = repository;
        }

        public Task AddDemo()
        {
            using (var tran = _repository.BeginTransaction())
            {
                _repository.InsertAsync(new Demo
                {
                    UserName = "李四",
                    Age = 26
                });

                tran.Commit();
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Demo>> DemoList()
        {
            return _repository.DemoList();
        }

        public Task<PagedResult<Demo>> DemoPageList(PageInfo page)
        {
            return _repository.GetPageList(page);
        }
    }
}
