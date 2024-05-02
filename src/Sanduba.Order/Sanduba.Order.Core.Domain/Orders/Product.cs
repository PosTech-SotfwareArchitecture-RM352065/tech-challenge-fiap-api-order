using Sanduba.Core.Domain.Common.Types;
using System;
using System.Collections.Generic;
namespace Sanduba.Core.Domain.Orders
{
    public sealed class Product : ValueObject
    {
        public Guid Id { get; set; }
        public double UnitPrice { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
