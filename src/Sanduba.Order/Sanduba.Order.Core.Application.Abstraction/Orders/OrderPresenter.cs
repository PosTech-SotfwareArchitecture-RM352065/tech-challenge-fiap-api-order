using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public abstract class OrderPresenter<T>
    {
        public abstract T Present(CreateOrderResponseModel responseModel);
        public abstract T Present(GetOrderResponseModel responseModel);
    }
}
