using System;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Carts.ResponseModel
{
    public record RemoveItemResponseModel(List<Guid> Products);
}
