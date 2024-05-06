using System;

namespace Sanduba.Core.Application.Abstraction.Payments.ResponseModel
{
    public record CreatePaymentResponseModel(Guid Id, string QrData);
}
