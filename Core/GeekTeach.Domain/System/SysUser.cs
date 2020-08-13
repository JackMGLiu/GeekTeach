using System;
using Dapper.Contrib.Extensions;
using Geek.Framework.Entity;
using Microsoft.VisualBasic.CompilerServices;

namespace GeekTeach.Domain.System
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class SysUser : IFullAuditEntity<string>, IStatusEntity<string>, ISoftDeleteEntity<string>
    {
        [ExplicitKey]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public int Gender { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        [Column(Name = "DelFlag")]
        public int IsDelete { get; set; }

        [Column(Name = "Status")]
        public int StatusCode { get; set; }

        [Column(Name = "CreateUserId")]
        public string CreateBy { get; set; }

        [Column(Name = "UpdateUserId")]
        public string ModiflyBy { get; set; }

        [Column(Name = "CreateDateTime")]
        public DateTime CreateTime { get; set; }

        [Column(Name = "UpdateDateTime")]
        public DateTime ModiflyTime { get; set; }

        public string Remark { get; set; }
    }
}
