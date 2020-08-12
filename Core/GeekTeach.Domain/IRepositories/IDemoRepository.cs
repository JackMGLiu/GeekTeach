using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain;

namespace GeekTeach.Domain.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDemoRepository : IRepository<Demo>
    {
        Task<IEnumerable<Demo>> DemoList();
    }
}
