using System;
using System.Collections.Generic;

namespace Sanduba.Infrastructure.API.Payment.Payments.Models
{
    public record Order(Guid Id, string Code, Guid ClientId, List<OrderItem> Items);
}
