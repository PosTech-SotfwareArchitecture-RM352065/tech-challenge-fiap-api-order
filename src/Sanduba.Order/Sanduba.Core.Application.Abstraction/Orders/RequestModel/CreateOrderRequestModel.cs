using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Orders.RequestModel
{
    public record Product(Guid Id, string Name, string Description, double UnitPrice, string Category);
    public record OrderItem(Product Product, double Amount);
    public record CreateOrderRequestModel
    {
        public Guid ClientId { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Method { get; set; }
        public string Provider { get; set; }
    };
}
