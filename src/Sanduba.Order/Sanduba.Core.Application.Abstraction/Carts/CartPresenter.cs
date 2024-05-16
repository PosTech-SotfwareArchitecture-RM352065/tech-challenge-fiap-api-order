using Sanduba.Core.Application.Abstraction.Carts.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Carts
{
    public abstract class CartPresenter<T>
    {
        public abstract T Present(AddItemResponseModel responseModel);
        public abstract T Present(GetSummaryResponseModel responseModel);
        public abstract T Present(List<GetSummaryResponseModel> responseModel);
    }
}
