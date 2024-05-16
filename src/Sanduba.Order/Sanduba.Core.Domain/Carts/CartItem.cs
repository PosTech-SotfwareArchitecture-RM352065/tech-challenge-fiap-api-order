using Sanduba.Core.Domain.Commons.Types;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Carts
{
    public class CartItem : ValueObject
    {
        public int Code { get; set; }
        public Product Product { get; set; }
        public double UnitPrice { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
