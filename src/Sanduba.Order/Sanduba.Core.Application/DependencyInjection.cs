﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Carts;
using Sanduba.Core.Application.Orders;

namespace Sanduba.Core.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOrderInteractor, OrderInteractor>();
            services.AddTransient<ICartInteractor, CartInteractor>();

            return services;
        }
    }
}
