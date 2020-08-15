using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Geek.Framework.Extensions
{
    public class ConfigExtensions
    {
        public static IConfiguration Configuration { get; set; }

        static ConfigExtensions()
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true, Optional = true })
                .Add(new JsonConfigurationSource { Path = "appsettings.Development.json", ReloadOnChange = true, Optional = true })
                .Build();
        }

        public static string GetJson(string jsonPath, string key)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile(jsonPath).Build();
            return config.GetSection(key).Value;
        }
    }
}
