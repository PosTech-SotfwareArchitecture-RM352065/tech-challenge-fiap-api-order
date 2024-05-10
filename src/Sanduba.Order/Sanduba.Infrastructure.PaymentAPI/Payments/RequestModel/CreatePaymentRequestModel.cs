using Sanduba.Infrastructure.PaymentAPI.Payments.Models;

namespace Sanduba.Infrastructure.PaymentAPI.Payments.RequestModel
{
    public record CreatePaymentRequestModel(Order Order, string Method, string Provider);
}
