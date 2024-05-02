using System;

namespace Sanduba.Core.Application.Abstraction.Orders.ResponseModel
{
    public record GetOrderResponseModel(Guid Id, int Code, double TotalAmount, string Status);
}
