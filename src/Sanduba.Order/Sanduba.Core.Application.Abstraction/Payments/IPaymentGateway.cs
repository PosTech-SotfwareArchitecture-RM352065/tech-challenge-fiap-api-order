using Sanduba.Core.Application.Abstraction.Payments.RequestModel;
using Sanduba.Core.Application.Abstraction.Payments.ResponseModel;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Payments
{
    public interface IPaymentGateway
    {
        public Task<CreatePaymentResponseModel> CreatePayment(CreatePaymentRequestModel requestModel);
    }
}
