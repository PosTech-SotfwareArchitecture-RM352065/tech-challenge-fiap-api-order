using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema
{
    [PrimaryKey(nameof(OrderId), nameof(Code))]
    public record OrderItem
    {
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null;

        [Required]
        public int Code { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public double ProductUnitPrice { get; set; }
    }
}
