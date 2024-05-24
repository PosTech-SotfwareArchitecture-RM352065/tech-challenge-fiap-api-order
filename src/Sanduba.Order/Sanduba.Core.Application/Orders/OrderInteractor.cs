using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Core.Application.Abstraction.Payments.RequestModel;
using Sanduba.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Product = Sanduba.Core.Domain.Orders.Product;

namespace Sanduba.Core.Application.Orders
{
    public sealed class OrderInteractor(
        IOrderPersistence orderPersistenceGateway,
        IPaymentGateway paymentGateway): IOrderInteractor
    {
        private readonly IOrderPersistence _orderPersistenceGateway = orderPersistenceGateway;
        private readonly IPaymentGateway _paymentGateway = paymentGateway;

        public CreateOrderResponseModel CreateOrder(CreateOrderRequestModel requestModel)
        {
            var nextCode = _orderPersistenceGateway.GetNextOrderCode();

            var newOrder = Order.CreateOrder(Guid.NewGuid(), requestModel.ClientId, nextCode);
            newOrder.AddItem(requestModel.Items
                                                .Select(item =>
                                                    new Product
                                                    {
                                                        Id = item.Product.Id,
                                                        Description = item.Product.Description,
                                                        Name = item.Product.Name,
                                                        Category = item.Product.Category,
                                                        UnitPrice = item.Product.UnitPrice
                                                    }).ToList());

            Enum.TryParse(requestModel.Method, out Domain.Payments.Method method);
            Enum.TryParse(requestModel.Provider, out Domain.Payments.Provider provider);

            var paymentPayload = new CreatePaymentRequestModel(newOrder, method, provider);

            var paymentRequest = _paymentGateway.CreatePayment(paymentPayload, CancellationToken.None);
            paymentRequest.Wait();

            newOrder.AddPayment(new Domain.Payments.Payment
            {
                Id = paymentRequest.Result.Id,
                Method = method,
                Provider = provider,
                Status = "CREATED"
            });

            _orderPersistenceGateway.SaveAsync(newOrder).Wait();

            return new CreateOrderResponseModel(newOrder.Id, nextCode, newOrder.Amount(), paymentRequest.Result.QrData);
        }

        public GetOrderResponseModel GetOrder(GetOrderRequestModel requestModel)
        {
            var query = _orderPersistenceGateway.GetByIdAsync(requestModel.id);
            query.Wait();

            var order = query.Result;

            return new GetOrderResponseModel(
                Id: order.Id,
                Code: (int)order.Code,
                Status: order.Status.ToString(),
                TotalAmount: order.Amount()
            );
        }

        public IEnumerable<GetOrderResponseModel> GetOrderByClientId(GetOrderByClientIdRequestModel requestModel)
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
                        TotalAmount: order.Amount()
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
                TotalAmount: order.Amount()
            )).ToList();
        }

        public UpdateOrderResponseModel OrderInProgress(UpdateStatusOrderResquestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public UpdateOrderResponseModel OrderReady(UpdateStatusOrderResquestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
