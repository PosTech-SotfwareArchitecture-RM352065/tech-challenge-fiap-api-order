using System;

namespace Sanduba.Core.Application.Abstraction.Orders.Events
{
    public record OrderPreparationConcludedEvent(Guid OrderId, DateTime ConcludedAt);
}
