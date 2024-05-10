namespace Sanduba.Infrastructure.PaymentAPI.Payments.ResponseModel
{
    public record CreatePaymentResponseModel(Guid Id, int Status, string ExternalId, string QrData);
}
