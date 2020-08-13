using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Geek.Framework.Db;
using GeekTeach.Domain.IRepositories.System;
using GeekTeach.Domain.System;

namespace GeekTeach.Data.Repositories.System
{
    public class SysUserRepository : Repository<SysUser, string>, ISysUserRepository
    {
        public SysUserRepository(Db db) : base(db)
        {

        }

        public Task<IEnumerable<SysUser>> UserList()
        {
            //Dapper.SqlBuilder builder = new Dapper.SqlBuilder();
            //var query = $@"select * from {typeof(Demo).Name} /**where**/ /**orderby**/ ";
            //var template = builder.AddTemplate(query);
            //builder.Where("Age=@a", new { a = obj });
            //builder.OrderBy("Id");
            //using (Db.Connection)
            //{
            //    var data = this.Db.Connection.QueryAsync<Demo>(template.RawSql, template.Parameters);
            //    return data;
            //}
            SqlBuilder builder = new SqlBuilder();
            var query = $@"select /**select**/ from {typeof(SysUser).Name} as u /**leftjoin**/ /**where**/ /**orderby**/ ";
            var template = builder.AddTemplate(query);
            builder.LeftJoin("RoleUser as ru on u.Id=ru.UserId");
            builder.LeftJoin("SysRole as r on r.Id=ru.RoleId");
            builder.Select("u.Id,u.RealName,r.RoleName");
            builder.Where("u.Id=@Id", new { Id = "11111" });
            var sql = template.RawSql;
            return null;
        }
    }
}
