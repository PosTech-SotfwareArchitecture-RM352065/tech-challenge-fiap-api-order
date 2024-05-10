using AutoMapper;
using Sanduba.API.Orders.Requests;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;


namespace Sanduba.API.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderApiCreateRequest, CreateOrderRequestModel>();
            CreateMap<OrderItemRequest, OrderItem>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
