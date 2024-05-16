using System;

namespace Sanduba.Core.Application.Abstraction.Carts.RequestModel
{
    public record AddItemRequestModel(Guid Id, Guid ProductId);
}
