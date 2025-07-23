using Microsoft.Extensions.DependencyInjection;
using DClean.Application.Interfaces;
using DClean.Domain.Settings;
using DClean.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;


namespace DClean.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {   
            //services.Configure<MailSettings>(config.GetSection("MailSettings"));
            //services.Configure<AppSettings>(config.GetSection("AppSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
