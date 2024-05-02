using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sanduba.Core.Application.Orders
{
    public sealed class OrderInteractor(IOrderPersistence orderPersistenceGateway, IPaymentGateway paymentGateway) 
        : IOrderInteractor
    {
        private readonly IOrderPersistence _orderPersistenceGateway = orderPersistenceGateway;
        private readonly IPaymentGateway _paymentGateway = paymentGateway;

        public CreateOrderResponseModel CreateOrder(CreateOrderRequestModel requestModel)
        {
            var nextCode = _orderPersistenceGateway.GetNextOrderCode();

            var newOrder = Order.CreateOrder(Guid.NewGuid(), requestModel.ClientId, nextCode);
            newOrder.AddItem(requestModel.Items
                                                .Select(item =>
                                                    new Product { 
                                                        Id = item.ProductId, 
                                                        UnitPrice = item.UnitPrice 
                                                    }).ToList());

            _orderPersistenceGateway.SaveAsync(newOrder).Wait();
            //_paymentGateway.CreatePayment();

            return new CreateOrderResponseModel(nextCode, newOrder.TotalAmount());
        }

        public GetOrderResponseModel GetOrder(GetOrderRequestModel requestModel)
        {
            var order = _orderPersistenceGateway.GetByIdAsync(requestModel.id).Result;

            return new GetOrderResponseModel(
                Id: order.Id,
                Code: (int)order.Code,
                Status: order.Status.ToString(),
                TotalAmount: order.TotalAmount()
            );
        }

        public IEnumerable<GetOrderResponseModel> GetOrderByClient(GetOrderByClientRequestModel requestModel)
        {
            var orders = _orderPersistenceGateway.GetOrdersByClient(requestModel.ClientId);
            var response = new List<GetOrderResponseModel>();

            foreach (var order in orders)
            {
                response.Add(
                    new GetOrderResponseModel(
                        Id: order.Id,
                        Code: (int)order.Code,
                        Status: order.Status.ToString(),
                        TotalAmount: order.TotalAmount()
                    ));
            }

            return response;
        }

        public IEnumerable<GetOrderResponseModel> GetOrderByStatus(GetOrderByStatusRequestModel requestModel)
        {
            Enum.TryParse(typeof(Status), requestModel.Status, out var status);

            var orders = _orderPersistenceGateway.GetOrdersByStatus((Status)status);

            return orders.Select(order => new GetOrderResponseModel(
                Id: order.Id,
                Code: (int)order.Code,
                Status: order.Status.ToString(),
                TotalAmount: order.TotalAmount()
            )).ToList();
        }

        public UpdateOrderResponseModel OrderInProgress(UpdateStatisOrderResquestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public UpdateOrderResponseModel OrderReady(UpdateStatisOrderResquestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
