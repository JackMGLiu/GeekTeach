using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories.System;
using GeekTeach.Domain.System;
using Microsoft.Extensions.Internal;

namespace GeekTeach.Application.Services.System
{
    public class SysUserService : ISysUserService
    {
        private readonly ISysUserRepository sysUserRepository;

        public SysUserService(ISysUserRepository sysUserRepository)
        {
            this.sysUserRepository = sysUserRepository;
        }

        public Task<IEnumerable<SysUser>> GetUsers()
        {
            return sysUserRepository.UserList();
        }
    }
}
