using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenVilla_Api.Helper.DependencyInjection
{
    public static class AppSettingsConfig
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<APISettings>(Configuration.GetSection("APISettings"));
            services.Configure<MailJetSettings>(Configuration.GetSection("MailJetSettings"));
            return services;
        }
    }
}
