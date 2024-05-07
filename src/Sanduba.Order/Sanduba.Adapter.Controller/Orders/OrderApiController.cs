using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;

namespace Sanduba.Adapter.Controller.Orders
{
    public sealed class OrderApiController : OrderController<string>
    {
        public OrderApiController(IOrderInteractor interactor, OrderPresenter<string> presenter) : base(interactor, presenter) { }

        public override string CreateOrder(CreateOrderRequestModel requestModel)
        {
            var responseModel = interactor.CreateOrder(requestModel);
            return presenter.Present(responseModel);
        }

        public override string GetOrder(GetOrderRequestModel requestModel)
        {
            var responseModel = interactor.GetOrder(requestModel);
            return presenter.Present(responseModel);
        }

        public override string GetOrderByClientId(GetOrderByClientIdRequestModel requestModel)
        {
            var responseModel = interactor.GetOrderByClientId(requestModel);
            return presenter.Present(responseModel);
        }
    }
}
