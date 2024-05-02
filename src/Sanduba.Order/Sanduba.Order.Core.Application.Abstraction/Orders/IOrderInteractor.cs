using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using System.Collections.Generic;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public interface IOrderInteractor
    {
        public IEnumerable<GetOrderResponseModel> GetOrderByStatus(GetOrderByStatusRequestModel requestModel);
        public GetOrderResponseModel GetOrder(GetOrderRequestModel requestModel);
        public IEnumerable<GetOrderResponseModel> GetOrderByClient(GetOrderByClientRequestModel requestModel);
        public CreateOrderResponseModel CreateOrder(CreateOrderRequestModel requestModel);
        public UpdateOrderResponseModel OrderInProgress(UpdateStatisOrderResquestModel requestModel);
        public UpdateOrderResponseModel OrderReady(UpdateStatisOrderResquestModel requestModel);
    }
}
