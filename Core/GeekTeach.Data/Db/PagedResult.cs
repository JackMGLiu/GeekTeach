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
        public virtual int Total { get; set; }
        public virtual IEnumerable<T> Items { get; set; }
    }
}
