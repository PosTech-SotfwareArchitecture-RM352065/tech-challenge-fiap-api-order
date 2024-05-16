using Sanduba.Core.Application.Abstraction.Carts.RequestModel;
using Sanduba.Core.Application.Abstraction.Carts.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Carts
{
    public interface ICartInteractor
    {
        public AddItemResponseModel AddItem(AddItemRequestModel requestModel);
        public GetSummaryResponseModel RemoveItem(RemoveItemRequestModel requestModel);
        public List<GetSummaryResponseModel> GetSummary(GetSummaryRequestModel requestModel);
        public GetSummaryResponseModel Checkout();
    }
}
