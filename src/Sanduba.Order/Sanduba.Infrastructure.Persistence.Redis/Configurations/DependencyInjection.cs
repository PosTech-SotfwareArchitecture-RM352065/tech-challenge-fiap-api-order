using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Infrastructure.Persistence.Redis.Carts;
using System;

namespace Sanduba.Infrastructure.Persistence.Redis.Configurations
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddRedisInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("CartDatabase:value") ?? string.Empty;

            services.AddScoped<ICartPersistenceGateway, CartRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
            });


            return services;
        }
    }
}
