using Microsoft.Extensions.Options;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Core.Application.Abstraction.Payments.RequestModel;
using Sanduba.Core.Application.Abstraction.Payments.ResponseModel;
using Sanduba.Infrastructure.PaymentAPI.Configurations.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanduba.Infrastructure.PaymentAPI.Payments
{
    public class PaymentApiGateway(IOptions<PaymentOptions> options) : IPaymentGateway
    {
        private readonly IOptions<PaymentOptions> _options = options;

        public CreatePaymentResponseModel CreatePayment(CreatePaymentRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
