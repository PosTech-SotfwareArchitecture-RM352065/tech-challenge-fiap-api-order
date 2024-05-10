using AutoMapper;
using OrderDomain = Sanduba.Core.Domain.Orders.Order;
using OrderGateway = Sanduba.Infrastructure.PaymentAPI.Payments.Models.Order;
using OrderItemDomain = Sanduba.Core.Domain.Orders.OrderItem;
using OrderItemGateway = Sanduba.Infrastructure.PaymentAPI.Payments.Models.OrderItem;
using ProductDomain = Sanduba.Core.Domain.Orders.Product;
using ProductGateway = Sanduba.Infrastructure.PaymentAPI.Payments.Models.Product;
using ApplicationPaymentRequest = Sanduba.Core.Application.Abstraction.Payments.RequestModel.CreatePaymentRequestModel;
using ApplicationPaymentResponse = Sanduba.Core.Application.Abstraction.Payments.ResponseModel.CreatePaymentResponseModel;
using GatewayPaymentRequest = Sanduba.Infrastructure.PaymentAPI.Payments.RequestModel.CreatePaymentRequestModel;
using GatewayPaymentResponse = Sanduba.Infrastructure.PaymentAPI.Payments.ResponseModel.CreatePaymentResponseModel;


namespace Sanduba.Infrastructure.PaymentAPI.Payments.Profiles
{
    public class PaymentGatewayProfile : Profile
    {
        public PaymentGatewayProfile() 
        {
            CreateMap<OrderDomain, OrderGateway>();
            CreateMap<OrderItemDomain, OrderItemGateway>();
            CreateMap<ProductDomain, ProductGateway>();
            //    .ForMember(destination => destination.Method, source => source.MapFrom(col => col.Method.ToString()))
            //    .ForMember(destination => destination.Provider, source => source.MapFrom(col => col.Provider.ToString()));
            CreateMap<ApplicationPaymentRequest, GatewayPaymentRequest>();
            CreateMap<GatewayPaymentResponse, ApplicationPaymentResponse>();
        }
    }
}
