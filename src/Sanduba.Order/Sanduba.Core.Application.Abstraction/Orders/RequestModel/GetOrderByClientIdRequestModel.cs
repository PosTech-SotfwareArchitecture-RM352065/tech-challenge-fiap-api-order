using System;

namespace Sanduba.Core.Application.Abstraction.Orders.RequestModel
{
    public record GetOrderByClientIdRequestModel(Guid ClientId);
}
