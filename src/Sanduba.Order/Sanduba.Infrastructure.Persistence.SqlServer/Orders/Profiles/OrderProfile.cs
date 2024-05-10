using AutoMapper;
using OrderDomain = Sanduba.Core.Domain.Orders.Order;
using OrderSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.Order;
using OrderItemDomain = Sanduba.Core.Domain.Orders.OrderItem;
using OrderItemSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.OrderItem;
using PaymentDomain = Sanduba.Core.Domain.Payments.Payment;
using PaymentSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.Payments;
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
                .ForPath(destination => destination.Product.Name, source => source.MapFrom(col => col.ProductName))
                .ForPath(destination => destination.Product.Description, source => source.MapFrom(col => col.ProductDescription))
                .ForPath(destination => destination.Product.Category, source => source.MapFrom(col => col.ProductCategory))
                .ForPath(destination => destination.Product.UnitPrice, source => source.MapFrom(col => col.ProductUnitPrice));
            CreateMap<ProductDomain, OrderItemSchema>();
            CreateMap<PaymentDomain, PaymentSchema>();
        }
    }
}
