using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Infrastructure.API.Payment.Configurations.Options;
using Sanduba.Infrastructure.API.Payment.Payments;
using System;

namespace Sanduba.Infrastructure.API.Payment.Configurations
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
            //services.AddServiceBusInfrastructure(configuration);

            return services;
        }

        //public static IServiceCollection AddServiceBusInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var entryAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        //    services.AddMassTransit(options =>
        //    {
        //        options.AddConsumer<PaymentUpdatedConsumer>();

        //        options.UsingAzureServiceBus((context, config) =>
        //        {
        //            config.Host(configuration["BrokerSettings:ConnectionStrings"]);

        //            config.SubscriptionEndpoint(
        //                configuration["BrokerSettings:SubscriptionName"],
        //                configuration["BrokerSettings:TopicName"],
        //                e =>
        //                {
        //                    e.UseMessageRetry(r => r.Interval(2, 10));
        //                    e.ConfigureConsumer<PaymentUpdatedConsumer>(context);
        //                });

        //            config.Message<PaymentUpdated>(x =>
        //            {
        //                x.SetEntityName(configuration["BrokerSettings:TopicName"]);
        //            });

        //        });
        //        options.SetDefaultRequestTimeout(TimeSpan.FromSeconds(15));
        //    });

        //    services.AddScoped<IPaymentGateway, PaymentGateway>();
        //    services.AddAutoMapper(entryAssemblies);

        //    return services;
        //}
    }
}
