using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Controller.ApiAdapter.Orders;
using Sanduba.Core.Application.Abstraction.Orders;

namespace Sanduba.Controller.ApiAdapter
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddApiAdapter(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<OrderController<string>, OrderApiController>();
            services.AddTransient<OrderPresenter<string>, OrderApiPresenter>();

            return services;
        }
    }
}
