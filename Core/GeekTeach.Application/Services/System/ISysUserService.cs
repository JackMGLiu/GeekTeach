using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeekTeach.Domain.System;

namespace GeekTeach.Application.Services.System
{
    public interface ISysUserService
    {
        Task<IEnumerable<SysUser>> GetUsers();
    }
}
