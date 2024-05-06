using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using System.Collections;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public abstract class OrderPresenter<T>
    {
        public abstract T Present(CreateOrderResponseModel responseModel);
        public abstract T Present(GetOrderResponseModel responseModel);
        public abstract T Present(IEnumerable<GetOrderResponseModel> responseModel);
    }
}
