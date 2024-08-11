using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.Events;
using Sanduba.Infrastructure.Broker.ServiceBus.Orders;
using Sanduba.Infrastructure.Persistence.SqlServer.Configurations;
using Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddServiceBusInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var entryAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddMassTransit(options =>
            {
                options.AddConsumers(typeof(DependencyInjection).Assembly);

                options.AddSagaStateMachine<OrderSaga, OrderSagaSchema>()
                    .EntityFrameworkRepository(ef => 
                    {
                        ef.ExistingDbContext<InfrastructureDbContext>();
                        ef.UseSqlServer();
                    });

                options.UsingAzureServiceBus((context, config) =>
                {
                    config.Host(configuration["BrokerSettings:ConnectionString"]);

                    config.SubscriptionEndpoint(
                        configuration["BrokerSettings:SubscriptionName"],
                        configuration["BrokerSettings:TopicName"],
                        e =>
                        {
                            e.UseMessageRetry(r => r.Interval(2, 10));
                            e.ConfigureConsumer<OrderBroker>(context);
                        });

                    config.UseInMemoryOutbox(context);


                    config.Message<OrderCreatedEvent>(x =>
                    {
                        x.SetEntityName(configuration["BrokerSettings:TopicName"]);
                    });

                    config.Message<OrderPreparationRequestedEvent>(x =>
                    {
                        x.SetEntityName(configuration["BrokerSettings:TopicName"]);
                    });

                    config.Message<OrderRejectedEvent>(x =>
                    {
                        x.SetEntityName(configuration["BrokerSettings:TopicName"]);
                    });

                    services.AddMassTransitHostedService();
                });

            });

            services.AddScoped<IOrderBroker, OrderBroker>();

            return services;
        }
    }
}