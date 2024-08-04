using Microsoft.AspNetCore.Mvc;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Core.Application.Abstraction.Carts.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Adapter.Mvc.Carts
{
    public sealed class CartApiPresenter : CartPresenter<IActionResult>
    {
        public override IActionResult Present(AddItemResponseModel responseModel)
        {
            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(GetSummaryResponseModel responseModel)
        {
            return new OkObjectResult(responseModel);
        }

        public override IActionResult Present(List<GetSummaryResponseModel> responseModel)
        {
            return new OkObjectResult(responseModel);
        }
    }
}
