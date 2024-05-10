using System;

namespace Sanduba.Core.Application.Abstraction.Orders.ResponseModel
{
    public record CreateOrderResponseModel(Guid Id, int Code, double TotalAmount, string PaymentRawData);
}
