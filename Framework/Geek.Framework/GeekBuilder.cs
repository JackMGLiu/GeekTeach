using Microsoft.Extensions.DependencyInjection;

namespace Geek.Framework
{
    /// <summary>
    /// 服务容器创建器
    /// </summary>
    public class GeekBuilder
    {
        public IServiceCollection Services { get; private set; }

        public GeekBuilder(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}
