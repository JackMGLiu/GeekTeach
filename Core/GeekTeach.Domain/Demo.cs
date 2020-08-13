using System;
using Dapper.Contrib.Extensions;
using Geek.Framework.Entity;

namespace GeekTeach.Domain
{
    /// <summary>
    /// 测试实体类
    /// </summary>
    [Table("Demo")]
    public class Demo : EntityBase<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }


    public class User : IAuditEntity, ISoftDeleteEntity
    {
        public DateTime CreateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ModiflyTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IsDelete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    }


}
