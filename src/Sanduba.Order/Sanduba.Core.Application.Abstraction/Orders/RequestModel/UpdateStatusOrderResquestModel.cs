using System;

namespace Sanduba.Core.Application.Abstraction.Orders.RequestModel
{
    public record UpdateStatusOrderResquestModel(Guid Id, string Status);
}
