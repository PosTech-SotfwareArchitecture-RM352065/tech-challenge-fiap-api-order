using MassTransit;
using System;
using Microsoft.Extensions.Logging;
using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.Events;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Orders
{
    [ExcludeFromCodeCoverage]
    public class OrderBroker(
        ILogger<OrderBroker> logger,
        IPublishEndpoint publishClient,
        IOrderPersistence orderPersistence
    ) : IConsumer<OrderPaymentConfirmedEvent>,
        IConsumer<OrderPaymentRejectedEvent>,
        IConsumer<OrderPreparationStartedEvent>,
        IConsumer<OrderPreparationConcludedEvent>,
        IOrderBroker 
    {
        private readonly ILogger<OrderBroker> _logger = logger;
        private readonly IPublishEndpoint _publishClient = publishClient;
        private readonly IOrderPersistence _orderPersistence = orderPersistence;

        public Task PublishOrderCreation(OrderCreatedEvent orderCreatedEvent)
        {
            try
            {
                return _publishClient.Publish<OrderCreatedEvent>(orderCreatedEvent);
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Consume(ConsumeContext<OrderPaymentConfirmedEvent> context)
        {
            try
            {
                _logger.LogInformation($"Message received id: {context.MessageId}");

                var order = _orderPersistence.GetByIdAsync(context.Message.OrderId).Result;

                order.Payed();

                _orderPersistence.UpdateAsync(order);

                return _publishClient.Publish<OrderPreparationRequestedEvent>(
                    new OrderPreparationRequestedEvent(
                        context.Message.OrderId,
                        (int)order.Code,
                        order.Status,
                        order.Amount(),
                        context.Message.PaymentId,
                        DateTime.UtcNow
                    ));
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Consume(ConsumeContext<OrderPaymentRejectedEvent> context)
        {
            try
            {
                _logger.LogInformation($"Message received id: {context.MessageId}");

                var order = _orderPersistence.GetByIdAsync(context.Message.OrderId).Result;

                order.Reject();

                _orderPersistence.UpdateAsync(order);

                return _publishClient.Publish<OrderRejectedEvent>
                    (new OrderRejectedEvent(context.Message.OrderId));
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Consume(ConsumeContext<OrderPreparationStartedEvent> context)
        {
            try
            {
                _logger.LogInformation($"Message received id: {context.MessageId}");

                var order = _orderPersistence.GetByIdAsync(context.Message.OrderId).Result;

                order.Accept();

                return _orderPersistence.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Consume(ConsumeContext<OrderPreparationConcludedEvent> context)
        {
            try
            {
                _logger.LogInformation($"Message received id: {context.MessageId}");

                var order = _orderPersistence.GetByIdAsync(context.Message.OrderId).Result;

                order.Ready();

                return _orderPersistence.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
