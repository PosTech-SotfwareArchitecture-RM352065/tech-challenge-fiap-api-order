using Sanduba.Core.Domain.Orders;
using Sanduba.Core.Domain.Payments;

namespace Sanduba.Core.Application.Abstraction.Payments.RequestModel
{
    public record CreatePaymentRequestModel(Order Order, Method Method, Provider Provider);
}
