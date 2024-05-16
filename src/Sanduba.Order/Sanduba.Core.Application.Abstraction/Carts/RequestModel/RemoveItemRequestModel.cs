using System;

namespace Sanduba.Core.Application.Abstraction.Carts.RequestModel
{
    public record RemoveItemRequestModel(Guid Id, Guid ProductId);
}
