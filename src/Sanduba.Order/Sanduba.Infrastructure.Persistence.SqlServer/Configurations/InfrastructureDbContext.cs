using Microsoft.EntityFrameworkCore;
using Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Configurations
{
    public class InfrastructureDbContext : DbContext
    {
        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options) { }

        internal DbSet<Order> Orders { get; set; }
        internal DbSet<OrderItem> OrderItems { get; set; }
        internal DbSet<OrderSagaSchema> SagaData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderSagaSchema>().HasKey(s => s.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
