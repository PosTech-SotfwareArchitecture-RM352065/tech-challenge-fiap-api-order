using System;
using System.ComponentModel.DataAnnotations;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema
{
    public record Payments
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Status { get; set; }
        
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null;
    }
}
