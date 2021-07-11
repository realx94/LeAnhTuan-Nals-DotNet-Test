using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Settings
{
    public static class AppSettingRegister
    {
        public static TSetting AddAppSetting<TSetting>(this IServiceCollection services, IConfiguration configuration, string PrimaryConfigSection)
            where TSetting : AppSettings
        {
            var configSection = configuration.GetSection(PrimaryConfigSection);
            services.Configure<TSetting>(configSection);

            var Settings = configSection.Get<TSetting>();

            services.AddSingleton(Settings);
            services.AddSingleton<TSetting>(Settings);

            return Settings;
        }
    }
}
