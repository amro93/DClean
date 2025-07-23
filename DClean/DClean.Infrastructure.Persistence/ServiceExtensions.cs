using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Exceptions;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Services;
using DClean.Application.Wrappers;
using DClean.Domain.Entities.Presistence.Identity;
using DClean.Domain.Settings;
using DClean.Infrastructure.Persistence.Contexts;
using DClean.Infrastructure.Persistence.Helpers;
using DClean.Infrastructure.Persistence.Models;
using DClean.Infrastructure.Persistence.Repositories;
using DClean.Infrastructure.Persistence.Services;
using DClean.Infrastructure.Persistence.Services.Identity;
using DClean.Infrastructure.Persistence.Services.Onboarding;
using DClean.Infrastructure.Persistence.Services.Tenants;
using DClean.Infrastructure.Shared.Services.StaticFiles;

namespace DClean.Infrastructure.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPresistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<DCleanDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IdentityDb");
                    options.UseTriggers(triggerOptions => triggerOptions.AddAssemblyTriggers());
                });
            }
            else
            {
                //services.AddEntityFrameworkMySql();
                //var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

                //services.AddDbContextPool<MainDbContext>((serviceProvider, options) =>
                //{
                //    options.UseMySql(configuration.GetConnectionString("DefaultMySqlConnection"), serverVersion,
                //        b =>
                //        {
                //            b.MigrationsAssembly(typeof(MainDbContext).Assembly.FullName);
                //        })
                //    .EnableSensitiveDataLogging();
                //});
                //services.AddTriggeredDbContextPool<DCleanDbContext>(options =>
                //{
                //    options.UseMySql(configuration.GetConnectionString("Default"), serverVersion,
                //        b =>
                //        {
                //            b.MigrationsAssembly(typeof(DCleanDbContext).Assembly.FullName);
                //        })
                //    .EnableSensitiveDataLogging();
                //    options.UseTriggers(triggerOptions => triggerOptions.AddAssemblyTriggers());
                //});
                services.AddTriggeredDbContextPool<DCleanDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(typeof(DCleanDbContext).Assembly.FullName);
                        })
                    .EnableSensitiveDataLogging();
                    options.UseTriggers(triggerOptions => triggerOptions.AddAssemblyTriggers());
                });

                services.AddScoped<IDCleanDbContextFactory, DCleanDbContextFactory>();
                services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped<ITenantService, TenantService>();
                services.AddScoped<ITenantConnectionStringService, TenantConnectionStringService>();
                services.AddScoped<IDemoRequestService, DemoRequestService>();
                services.AddScoped<IStaticFileHelper, StaticFileHelper>();
                services.AddScoped<IRoleService, RoleService>();
            }

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn = new()
                {
                    RequireConfirmedAccount = false,
                    RequireConfirmedEmail = true,
                    RequireConfirmedPhoneNumber = false
                };
                options.Password = new()
                {
                    RequireDigit = false,
                    RequiredUniqueChars = 0,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
                //options.ClaimsIdentity = new()
                //{

                //};
                //options.Tokens;
                options.Stores = new StoreOptions()
                {
                    ProtectPersonalData = false,
                };
                options.User = new UserOptions { RequireUniqueEmail = true };
            })
                .AddEntityFrameworkStores<DCleanDbContext>()
                .AddDefaultTokenProviders();
            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
            services.AddSeedServices();
        }

        internal static void AddSeedServices(this IServiceCollection services)
        {
            services.AddTransient<ISeedService, Seeds.RolesSeed>();
            services.AddTransient<ISeedService, Seeds.SuperAdminSeed>();
            services.AddTransient<ISeedService, Seeds.BasicUserSeed>();
            services.AddTransient<ISeedService, Seeds.DefaultTenantSeed>();
            services.AddTransient<ISeedService, Seeds.TenantAdminSeed>();
        }

        public static async Task UsePresistenceInfrastructureAsync(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            var config = services.GetRequiredService<IConfiguration>();
            var appSettingsSection = config.GetSection("ApplicationSettings");
            if (!appSettingsSection.Exists() || !appSettingsSection.GetValue<bool>("Seed", false)) return;
            using (var scope = services.CreateScope())
            {

                {
                    var seedServices = scope.ServiceProvider.GetRequiredService<IEnumerable<ISeedService>>();
                    foreach (var seedService in seedServices.OrderBy(t => t.Order))
                    {
                        await seedService.SeedAsync();
                    }
                }
            }

        }
    }
}
