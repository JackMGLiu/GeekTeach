using System;
using System.Collections.Generic;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// 分页结果对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public PagedResult()
        {
        }

        public PagedResult(int page, int size)
        {
            Page = page;
            Size = size;
        }

        public virtual int Page { get; set; } = 1;
        public virtual int Size { get; set; } = 15;
        public virtual long Total { get; set; }
        public virtual IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 总的分页数
        /// </summary>
        public long TotalPage
        {
            get
            {
                if (this.Page > 0 && this.Size > 0)
                {
                    return (int)Math.Ceiling((decimal)this.Page / this.Size);
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class PageInfo
    {
        /// <summary>
        /// 要查询的表名
        /// </summary>
        public string tableName { get; set; }
        /// <summary>
        /// 要查询的字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string where { get; set; }
        /// <summary>
        /// 排序的字段
        /// </summary>
        public string orderFiled { get; set; }
        /// <summary>
        /// 排序的方法 desc 或者是 asc
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 每页的数据条数
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 需要传递给dapper的参数化内容
        /// </summary>
        public object paramsObj { get; set; }
    }
}
