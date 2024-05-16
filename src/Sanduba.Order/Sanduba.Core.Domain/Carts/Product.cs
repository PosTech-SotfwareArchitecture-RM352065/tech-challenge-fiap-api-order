using Sanduba.Core.Domain.Commons.Types;
using System;

namespace Sanduba.Core.Domain.Carts
{
    public sealed class Product(Guid id) : Entity<Guid>(id)
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public double UnitPrice { get; init; }

        public Category Category { get; init; }

        public override void ValidateEntity()
        {
            throw new NotImplementedException();
        }
    }
}
