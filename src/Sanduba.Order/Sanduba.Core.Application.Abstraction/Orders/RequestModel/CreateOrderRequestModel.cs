using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Orders.RequestModel
{
    public record OrderItem(Guid ProductId, double UnitPrice);
    public record CreateOrderRequestModel(Guid ClientId, List<OrderItem> Items);
}
