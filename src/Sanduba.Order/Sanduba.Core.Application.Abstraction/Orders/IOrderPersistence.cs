using Sanduba.Core.Application.Abstraction.Commons;
using Sanduba.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public interface IOrderPersistence : IAsyncPersistenceGateway<Guid, Order>
    {
        public int GetNextOrderCode(CancellationToken cancellationToken = default);
        public IEnumerable<Order> GetOrdersByStatus(Status status, CancellationToken cancellationToken = default);
        public IEnumerable<Order> GetOrdersByClient(Guid clientId, CancellationToken cancellationToken = default);
    }
}
