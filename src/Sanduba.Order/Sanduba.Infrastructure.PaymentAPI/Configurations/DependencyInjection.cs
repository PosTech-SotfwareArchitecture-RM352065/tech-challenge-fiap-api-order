using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Infrastructure.PaymentAPI.Configurations.Options;
using Sanduba.Infrastructure.PaymentAPI.Payments;

namespace Sanduba.Infrastructure.PaymentAPI.Configurations
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>

        public static IServiceCollection AddPaymentInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IPaymentGateway, PaymentGateway>();
            services.AddOptions().ConfigureOptions<PaymentConfigureOptions>();

            return services;
        }
    }
}
