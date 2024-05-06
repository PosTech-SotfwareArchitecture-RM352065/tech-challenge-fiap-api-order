using System;
using System.Collections.Generic;
using Sanduba.Core.Domain.Common.Events;

namespace Sanduba.Core.Domain.Common.Types
{
    public abstract class Entity<TId>
    {
        public TId Id { get; private init; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public abstract void ValidateEntity();

        private readonly List<DomainEvent> _events = new();

        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
    }
}
