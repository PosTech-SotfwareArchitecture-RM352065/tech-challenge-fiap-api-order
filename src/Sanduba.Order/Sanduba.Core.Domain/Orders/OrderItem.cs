using Sanduba.Core.Domain.Common.Types;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Orders
{
    public class OrderItem : ValueObject
    {
        public int Code { get; set; }
        public Product Product { get; set; }
        public double Amount => Product.UnitPrice;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
