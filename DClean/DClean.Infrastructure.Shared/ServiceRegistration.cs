using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DClean.Application.Interfaces;
using DClean.Domain.Settings;
using DClean.Infrastructure.Shared.Services;

namespace DClean.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.Configure<AppSettings>(_config.GetSection("AppSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
