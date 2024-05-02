using Sanduba.Core.Application.Abstraction.Payments.RequestModel;
using Sanduba.Core.Application.Abstraction.Payments.ResponseModel;

namespace Sanduba.Core.Application.Abstraction.Payments
{
    public interface IPaymentGateway
    {
        public CreatePaymentResponseModel CreatePayment(CreatePaymentRequestModel requestModel);
    }
}
