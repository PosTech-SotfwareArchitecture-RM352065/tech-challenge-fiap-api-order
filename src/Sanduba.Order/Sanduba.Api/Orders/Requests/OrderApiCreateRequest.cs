using System.Collections.Generic;
using System;

namespace Sanduba.API.Orders.Requests
{
    public record ProductRequest(Guid Id, string Name, string Description, double UnitPrice, string Category);
    public record OrderItemRequest(ProductRequest Product, string Currency, double Amount);
    public record OrderApiCreateRequest(List<OrderItemRequest> Items, string Method, string Provider);
}
