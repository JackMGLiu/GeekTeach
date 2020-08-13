using System;
using Dapper;
using Geek.Framework.Entity;
using GeekTeach.Domain.System;

namespace GeekTeach.Domain
{
    public class ColumnMapper
    {
        public static void SetMapper()
        {
            SqlMapper.SetTypeMap(typeof(SysUser), new ColumnAttributeTypeMapper<SysUser>());
            //每个需要用到[colmun(Name="")]特性的model，都要在这里添加映射
        }
    }
}
