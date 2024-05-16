using Sanduba.Core.Domain.Commons.Types;
using System;
using System.Collections.Generic;
namespace Sanduba.Core.Domain.Orders
{
    public sealed class Product : ValueObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double UnitPrice { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
