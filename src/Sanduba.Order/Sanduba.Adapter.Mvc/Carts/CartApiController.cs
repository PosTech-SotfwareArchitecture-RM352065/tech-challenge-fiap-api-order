using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Core.Application.Abstraction.Carts.RequestModel;

namespace Sanduba.Adapter.Mvc.Carts
{
    public sealed class CartApiController : CartController<IActionResult>
    {
        private new readonly ICartInteractor interactor;
        private new readonly CartPresenter<IActionResult> presenter;

        public CartApiController(ICartInteractor interactor, CartPresenter<IActionResult> presenter) : base(interactor, presenter)
        {
            this.interactor = interactor;
            this.presenter = presenter;
        }

        public override IActionResult AddItem(AddItemRequestModel requestModel)
        {
            var responseModel = interactor.AddItem(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult Checkout()
        {
            var responseModel = interactor.Checkout();
            return presenter.Present(responseModel);
        }

        public override IActionResult GetSummary(GetSummaryRequestModel requestModel)
        {
            var responseModel = interactor.GetSummary(requestModel);
            return presenter.Present(responseModel);
        }

        public override IActionResult RemoveItem(RemoveItemRequestModel requestModel)
        {
            var responseModel = interactor.RemoveItem(requestModel);
            return presenter.Present(responseModel);
        }
    }
}
