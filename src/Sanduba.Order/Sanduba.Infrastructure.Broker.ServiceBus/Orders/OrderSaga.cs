using MassTransit;
using Sanduba.Core.Application.Abstraction.Orders.Events;
using Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Orders
{
    public class OrderSaga : MassTransitStateMachine<OrderSagaSchema>
    {
        public State Created { get; set; }
        public State WaitingPayment { get; set; }
        public State Payed { get; set; }
        public State WaitingConfirmation { get; set; }
        public State Rejected { get; set; }
        public State Accepted { get; set; }
        public State Canceled { get; set; }

        public Event<OrderCreatedEvent> OrderCreated { get; set; }
        public Event<OrderPaymentConfirmedEvent> OrderPaymentConfirmed { get; set; }
        public Event<OrderPaymentRejectedEvent> OrderPaymentRejected { get; set; }
        public Event<OrderPreparationRequestedEvent> OrderPreparationRequested { get; set; }
        public Event<OrderAcceptedEvent> OrderAccepted { get; set; }
        public Event<OrderPreparationStartedEvent> OrderPreparationStarted { get; set; }

        public OrderSaga()
        {
            InstanceState(x => Created);

            Event(() => OrderCreated, e => e.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPaymentConfirmed, e => e.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPaymentRejected, e => e.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPreparationRequested, e => e.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderAccepted, e => e.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPreparationStarted, e => e.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderCreated)
                    .Then(context => context.Saga.OrderId = context.Message.OrderId)
                    .TransitionTo(WaitingPayment)
                    .Publish(context => new OrderCreatedEvent(context.Message.OrderId))
                );

            During(WaitingPayment,
                When(OrderPaymentConfirmed)
                    .Then(context => context.Saga.OrderId = context.Message.OrderId)
                    .TransitionTo(Payed)
                    .Publish(context => new OrderPaymentConfirmedEvent(
                        context.Message.OrderId,
                        context.Message.PaymentId,
                        context.Message.Method,
                        context.Message.Provider
                    ))
                );

            During(WaitingPayment,
                When(OrderPaymentRejected)
                    .Then(context => context.Saga.OrderId = context.Message.OrderId)
                    .TransitionTo(Rejected)
                    .Publish(context => new OrderPaymentRejectedEvent(
                        context.Message.OrderId,
                        context.Message.Comments
                    ))
                );

            During(Payed,
                When(OrderPreparationRequested)
                    .Then(context => context.Saga.OrderId = context.Message.OrderId)
                    .TransitionTo(WaitingConfirmation)
                    .Publish(context => new OrderPreparationRequestedEvent(
                        context.Message.OrderId,
                        context.Message.Code,
                        context.Message.Status,
                        context.Message.TotalAmount,
                        context.Message.PaymentId, 
                        context.Message.PayedAt
                    ))
                );

            During(WaitingConfirmation,
                When(OrderPreparationStarted)
                    .Then(context => context.Saga.OrderId = context.Message.OrderId)
                    .TransitionTo(Accepted)
                    .Publish(context => new OrderPreparationStartedEvent(
                        context.Message.OrderId,
                        context.Message.DeliveryEstimation
                    ))
                .Finalize()
                );
        }
    }
}
