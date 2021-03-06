﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories;
using GeekTeach.Domain;
using Dapper.Contrib.Extensions;

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

        public Task<IEnumerable<Demo>> DemoListByWhere(int obj)
        {
            return _repository.DemoListByWhere(obj);
        }

        public Task<PagedResult<Demo>> DemoPageList(PageInfo page)
        {
            return _repository.GetPageList(page);
        }

        public Task<Demo> GetModel(object key)
        {
            var data = _repository.Db.Connection.GetAsync<Demo>(key);
            return data;
        }
    }
}
