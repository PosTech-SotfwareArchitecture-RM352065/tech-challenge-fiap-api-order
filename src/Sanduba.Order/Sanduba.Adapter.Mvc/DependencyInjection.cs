using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Adapter.Mvc.Carts;
using Sanduba.Adapter.Mvc.Orders;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Core.Application.Abstraction.Orders;

namespace Sanduba.Adapter.Mvc
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddMvcAdapter(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<OrderController<IActionResult>, OrderApiController>();
            services.AddTransient<OrderPresenter<IActionResult>, OrderApiPresenter>();
            services.AddTransient<CartController<IActionResult>, CartApiController>();
            services.AddTransient<CartPresenter<IActionResult>, CartApiPresenter>();

            return services;
        }
    }
}

