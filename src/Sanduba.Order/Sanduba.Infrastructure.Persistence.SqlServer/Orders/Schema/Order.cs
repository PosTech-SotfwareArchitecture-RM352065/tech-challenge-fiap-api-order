using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema
{
    public record Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public int Status { get; set; }

        public List<OrderItem> Items { get; set; } = new();
        public List<Payments> Payments { get; set; } = new();
    }
}
