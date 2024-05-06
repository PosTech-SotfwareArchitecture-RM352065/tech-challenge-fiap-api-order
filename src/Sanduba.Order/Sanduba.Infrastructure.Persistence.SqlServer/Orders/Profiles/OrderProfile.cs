using AutoMapper;
using OrderDomain = Sanduba.Core.Domain.Orders.Order;
using OrderSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.Order;
using OrderItemDomain = Sanduba.Core.Domain.Orders.Order.OrderItem;
using OrderItemSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.OrderItem;
using ProductDomain = Sanduba.Core.Domain.Orders.Product;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDomain, OrderSchema>();
            CreateMap<OrderSchema, OrderDomain>();
            CreateMap<OrderItemDomain, OrderItemSchema>()
                .IncludeMembers(source => source.Product);
            CreateMap<OrderItemSchema, OrderItemDomain>()
                .ForPath(destination => destination.Product.Id, source => source.MapFrom(col => col.ProductId))
                .ForPath(destination => destination.Product.UnitPrice, source => source.MapFrom(col => col.ProductUnitPrice));
            CreateMap<ProductDomain, OrderItemSchema>();
        }
    }
}
