using System;

namespace Sanduba.Infrastructure.API.Payment.Payments.ResponseModel
{
    public record CreatePaymentResponseModel(Guid Id, int Status, string ExternalId, string QrData);
}
