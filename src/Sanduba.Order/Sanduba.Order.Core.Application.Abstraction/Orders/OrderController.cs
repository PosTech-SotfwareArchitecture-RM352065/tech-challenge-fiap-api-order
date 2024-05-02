using Sanduba.Core.Application.Abstraction.Orders.RequestModel;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public abstract class OrderController<T>
    {
        protected readonly IOrderInteractor interactor;
        protected readonly OrderPresenter<T> presenter;

        protected OrderController(IOrderInteractor interactor, OrderPresenter<T> presenter)
        {
            this.interactor = interactor;
            this.presenter = presenter;
        }

        public abstract T CreateOrder(CreateOrderRequestModel requestModel);
        public abstract T GetOrder(GetOrderRequestModel requestModel);
    }
}
