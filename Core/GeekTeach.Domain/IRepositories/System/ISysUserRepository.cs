using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.System;

namespace GeekTeach.Domain.IRepositories.System
{
    public interface ISysUserRepository : IRepository<SysUser, string>
    {
        Task<IEnumerable<SysUser>> UserList();
    }
}
