using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Configurations
{
    internal class OrderDbContextConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

        }
    }
}
