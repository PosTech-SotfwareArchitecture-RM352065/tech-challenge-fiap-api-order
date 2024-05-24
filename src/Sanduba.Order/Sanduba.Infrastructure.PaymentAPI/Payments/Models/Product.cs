using System;

namespace Sanduba.Infrastructure.API.Payment.Payments.Models
{
    public record Product(Guid Id, string Name, string Description, double UnitPrice, string Category);
}