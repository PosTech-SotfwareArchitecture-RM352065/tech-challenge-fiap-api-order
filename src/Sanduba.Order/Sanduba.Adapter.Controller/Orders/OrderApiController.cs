using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;

namespace Sanduba.Adapter.Controller.Orders
{
    public sealed class OrderApiController : OrderController<IActionResult>
    {
        public OrderApiController(IOrderInteractor interactor, OrderPresenter<IActionResult> presenter) : base(interactor, presenter) { }

        public override IActionResult CreateOrder(CreateOrderRequestModel requestModel)
        {
            var responseModel = interactor.CreateOrder(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetOrder(GetOrderRequestModel requestModel)
        {
            var responseModel = interactor.GetOrder(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult GetOrderByClientId(GetOrderByClientIdRequestModel requestModel)
        {
            var responseModel = interactor.GetOrderByClientId(requestModel);
            return presenter.Present(responseModel);
        }
    }
}
