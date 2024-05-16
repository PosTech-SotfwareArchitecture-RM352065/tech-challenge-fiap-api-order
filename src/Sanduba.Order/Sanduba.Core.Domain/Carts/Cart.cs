using Sanduba.Core.Domain.Commons.Types;
using System;
using System.Collections.Generic;


namespace Sanduba.Core.Domain.Carts
{
    public sealed class Cart(Guid id) : Entity<Guid>(id)
    {
        private readonly List<CartItem> _items = new();

        public IReadOnlyCollection<CartItem> Items => _items;

        public Guid ClientId { get; init; }

        public static Cart CreateCart(Guid id, Guid clientId)
        {
            var carrinho = new Cart(id)
            {
                ClientId = clientId,
            };

            return carrinho;
        }

        public override void ValidateEntity()
        {

        }
    }
}
