using Geek.Framework.Entity;

namespace GeekTeach.Domain
{
    public class Demo : IEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }

}
