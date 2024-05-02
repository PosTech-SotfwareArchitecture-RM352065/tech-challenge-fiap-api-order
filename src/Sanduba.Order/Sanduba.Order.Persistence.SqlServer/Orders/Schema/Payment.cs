using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema
{
    public record Payment
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
