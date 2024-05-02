using AutoMapper;
using OrderDomain = Sanduba.Core.Domain.Orders.Order;
using OrderSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.Order;
using OrderItemDomain = Sanduba.Core.Domain.Orders.Order.OrderItem;
using OrderItemSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.OrderItem;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDomain, OrderSchema>();
            CreateMap<OrderItemDomain, OrderItemSchema>();
        }
    }
}
