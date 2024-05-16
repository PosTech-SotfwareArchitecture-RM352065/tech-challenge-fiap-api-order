using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Carts
{
    public interface ICartPersistenceGateway
    {
        public void AddItem(Guid id, Guid product);
        public void RemoveItem(Guid id, Guid product);
        public List<Guid> GetSummary(Guid id);
    }
}
