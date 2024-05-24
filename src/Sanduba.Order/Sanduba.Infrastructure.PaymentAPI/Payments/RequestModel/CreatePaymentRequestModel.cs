using Sanduba.Infrastructure.API.Payment.Payments.Models;

namespace Sanduba.Infrastructure.API.Payment.Payments.RequestModel
{
    public record CreatePaymentRequestModel(Order Order, string Method, string Provider);
}
