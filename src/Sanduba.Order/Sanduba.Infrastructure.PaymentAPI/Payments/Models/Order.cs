namespace Sanduba.Infrastructure.PaymentAPI.Payments.Models
{
    public record Order(Guid Id, string Code, Guid ClientId, List<OrderItem> Items);
}
