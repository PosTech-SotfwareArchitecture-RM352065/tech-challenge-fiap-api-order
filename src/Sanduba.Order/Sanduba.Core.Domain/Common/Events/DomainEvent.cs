using System;

namespace Sanduba.Core.Domain.Common.Events
{
    public record DomainEvent
    {
        public DateTimeOffset OccurredAt { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
