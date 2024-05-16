using Sanduba.Core.Application.Abstraction.Carts.RequestModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Carts
{
    public abstract class CartController<T>
    {
        protected readonly ICartInteractor interactor;
        protected readonly CartPresenter<T> presenter;

        protected CartController(ICartInteractor interactor, CartPresenter<T> presenter)
        {
            this.interactor = interactor;
            this.presenter = presenter;
        }

        public abstract T AddItem(AddItemRequestModel requestModel);
        public abstract T RemoveItem(RemoveItemRequestModel requestModel);
        public abstract T GetSummary(GetSummaryRequestModel requestModel);
        public abstract T Checkout();

    }
}
