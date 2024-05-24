using Moq;
using Sanduba.Core.Application.Abstraction.Orders;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Core.Application.Orders;
using Sanduba.Core.Domain.Orders;
using OrderItem = Sanduba.Core.Application.Abstraction.Orders.RequestModel.OrderItem;
using Product = Sanduba.Core.Application.Abstraction.Orders.RequestModel.Product;

namespace Sanduba.Order.UnitTests.Core.Domain
{
    public class OrderApplicationTest
    {
        //// add tests here 
        //public OrderApplicationTest()
        //{
        //    [Fact]
        //    public void CreateOrder_WhenCalled_ShouldCreateOrder()
        //    {
        //        // Arrange
        //        var orderPersistenceGateway = new Mock<IOrderPersistence>();
        //        var paymentGateway = new Mock<IPaymentGateway>();
        //        var orderInteractor = new OrderInteractor(orderPersistenceGateway.Object, paymentGateway.Object);

        //        var requestModel = new CreateOrderRequestModel
        //        {
        //            ClientId = Guid.NewGuid(),
        //            Items = new List<OrderItem>
        //            {
        //                new OrderItem
        //                (
        //                    Product: new Product
        //                    (
        //                        Id: Guid.NewGuid(),
        //                        Description:  "Product Description",
        //                        Name: "Product Name",
        //                        Category: "Product Category",
        //                        UnitPrice: 10.0
        //                    ),
        //                    Amount: 1
        //                )
        //            },
        //            Method = "Method",
        //            Provider = "Provider"
        //        };

        //        var nextCode = 1;
        //        orderPersistenceGateway.Setup(x => x.GetNextOrderCode()).Returns(nextCode);

        //        var newOrder = Order.CreateOrder(Guid.NewGuid(), requestModel.ClientId, nextCode);
        //        newOrder.AddItem(requestModel.Items
        //                                                   .Select(item =>
        //                                                                                              new Product
        //                                                                                              {
        //                                                                                                  Id = item.Product.Id,
        //                                                                                                  Description = item.Product.Description,
        //                                                                                                  Name = item.Product.Name,
        //                                                                                                  Category = item.Product.Category,
        //                                                                                                  UnitPrice = item.Product.UnitPrice
        //                                                                                              }).ToList());

        //        var method = Payments.Method.CREDIT_CARD;
        //        var provider = Payments.Provider.PAYPAL;

        //        var paymentPayload = new CreatePaymentRequestModel(newOrder, method, provider);

        //        var paymentRequest = new PaymentRequest
        //        {
        //            Id = Guid.NewGuid(),
        //            QrData = "QrData"
        //        };

        //        paymentGateway.Setup(x => x.CreatePayment(paymentPayload)).ReturnsAsync(paymentRequest);

        //        var payment = new Domain.Payments.Payment
        //        {
        //            Id = paymentRequest.Id,
        //            Method = method,
        //            Provider = provider,
        //            Status = "CREATED"
        //        };

        //        newOrder.AddPayment(payment);

        //        orderPersistenceGateway.Setup(x => x.SaveAsync(newOrder)).Returns(Task.CompletedTask);

        //        // Act
        //        var result = orderInteractor.CreateOrder(requestModel);

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equal(newOrder.Id, result.Id);
        //        Assert.Equal(nextCode, result.Code);
        //        Assert.Equal(newOrder.Amount(), result.Amount);
        //        Assert.Equal(paymentRequest.QrData, result.QrData);
        //    }
        //}

    }
}
