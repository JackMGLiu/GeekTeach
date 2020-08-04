using Microsoft.Extensions.DependencyInjection;

namespace Geek.Framework
{
    public class GeekBuilder
    {
        public IServiceCollection Services { get; private set; }

        public GeekBuilder(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}
