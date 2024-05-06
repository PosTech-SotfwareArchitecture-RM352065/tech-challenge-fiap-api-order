using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace Sanduba.Controller.ApiAdapter.Orders
{
    public sealed class OrderApiPresenter : OrderPresenter<string>
    {
        private string SerializeToJsonString(object obj)
        {
            if (obj is null) return string.Empty;

            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch
            {
                throw;
            }
        }

        public override string Present(CreateOrderResponseModel responseModel)
        {
            return SerializeToJsonString(responseModel);
        }

        public override string Present(GetOrderResponseModel responseModel)
        {
            return SerializeToJsonString(responseModel);
        }

        public override string Present(IEnumerable<GetOrderResponseModel> responseModel)
        {
            return SerializeToJsonString(responseModel);
        }
    }
}
