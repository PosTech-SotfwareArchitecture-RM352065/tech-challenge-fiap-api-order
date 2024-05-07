using System.Collections.Generic;
using System;

namespace Sanduba.API.Orders.Requests
{
    public record OrderItem(Guid ProductId, double UnitPrice);
    public record CreateOrderApiRequest(List<OrderItem> Items);
}
