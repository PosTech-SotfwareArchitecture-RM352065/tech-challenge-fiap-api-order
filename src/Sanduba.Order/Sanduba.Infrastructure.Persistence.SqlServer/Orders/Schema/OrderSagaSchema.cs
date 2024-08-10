using MassTransit;
using System;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema
{
    public class OrderSagaSchema : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid OrderId { get; set; }
    }
}
