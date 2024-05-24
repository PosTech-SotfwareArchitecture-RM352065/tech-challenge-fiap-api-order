namespace Sanduba.Infrastructure.API.Payment.Payments.Models
{
    public record OrderItem(int Code, Product Product, double Amount, string Currency = "BRL");
}
