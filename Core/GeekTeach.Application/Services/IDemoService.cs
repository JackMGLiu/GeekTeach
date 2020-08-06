using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain;

namespace GeekTeach.Application.Services
{
    public interface IDemoService
    {
        Task<IEnumerable<Demo>> DemoList();

        Task AddDemo();

        Task<PagedResult<Demo>> DemoPageList(PageInfo page);
    }
}
