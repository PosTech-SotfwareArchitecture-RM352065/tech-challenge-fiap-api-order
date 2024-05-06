using System;

namespace Sanduba.Core.Domain.Common.Types
{
    public abstract class AggregateRoot : Entity<Guid>
    {
        protected AggregateRoot(Guid id) : base(id) { }
    }
}
