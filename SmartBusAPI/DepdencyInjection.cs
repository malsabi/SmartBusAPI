using SmartBusAPI.Services;
using SmartBusAPI.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using SmartBusAPI.Persistence.Repositories;
using SmartBusAPI.Common.Interfaces.Services;
using SmartBusAPI.Common.Interfaces.Repositories;

namespace SmartBusAPI
{
    public static class DepdencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IHashProviderService, HashProviderService>();
            services.AddSingleton<IJwtAuthService, JwtAuthService>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<SmartBusContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB_CONNECTION_STRING"));
            });

            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IBusDriverRepository, BusDriverRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}