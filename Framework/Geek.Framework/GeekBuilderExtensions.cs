using System;
using Microsoft.Extensions.DependencyInjection;

namespace Geek.Framework
{
    /// <summary>
    /// 服务启动扩展
    /// </summary>
    public static class GeekBuilderExtensions
    {
        public static GeekBuilder AddGeek(this IServiceCollection services, Action<GeekOptions> setup = null)
        {
            var options = new GeekOptions();
            setup?.Invoke(options);

            return new GeekBuilder(services);
        }
    }
}
