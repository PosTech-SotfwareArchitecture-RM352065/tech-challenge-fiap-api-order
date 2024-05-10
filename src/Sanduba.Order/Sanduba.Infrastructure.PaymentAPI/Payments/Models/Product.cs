namespace Sanduba.Infrastructure.PaymentAPI.Payments.Models
{
    public record Product(Guid Id, string Name, string Description, double UnitPrice, string Category);
}