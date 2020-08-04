using System;
using Geek.Framework.Entity;

namespace GeekTeach.Web.Models
{
    public class Demo : IEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
