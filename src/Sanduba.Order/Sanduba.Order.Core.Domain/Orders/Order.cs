using Sanduba.Core.Domain.Common.Exceptions;
using Sanduba.Core.Domain.Common.Assertions;
using Sanduba.Core.Domain.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Sanduba.Core.Domain.Payments;

namespace Sanduba.Core.Domain.Orders
{
    public sealed class Order : Entity<Guid>
    {
        public class OrderItem : ValueObject
        {
            public int Code { get; init; }
            public Product Product { get; init; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Code;
            }
        }

        public int? Code { get; init; }

        private Order(Guid id) : base(id) { }

        private readonly List<OrderItem> _items = new();

        public IReadOnlyCollection<OrderItem> Items => _items;

        public Guid ClientId { get; init; }

        public Status Status { get; private set; }

        private readonly List<Payment> _payments = new();

        public IReadOnlyCollection<Payment> Payments => _payments;

        public static Order CreateOrder(Guid id, Guid clientId, int? code = null)
        {
            var order = new Order(id)
            {
                Code = code,
                ClientId = clientId,
                Status = Status.Created
            };

            return order;
        }

        public void Accept()
        {
            AssertionConcern.AssertArgumentEqual(Status, Status.Payed, "Pedido deve estar com status de PAGO");

            Status = Status.Accepted;
        }

        public void Reject()
        {
            AssertionConcern.AssertArgumentEqual(Status, Status.Payed, "Pedido deve estar com status de PAGO");

            Status = Status.Reject;
        }

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);

            AssertionConcern.AssertArgumentEqual(Status, Status.Created, "Pedido deve estar com status de CRIADO");
            Status = Status.WaitingPayment;
        }

        public void Cancel()
        {
            AssertionConcern.AssertArgumentNotEqual(Status, Status, "Pedido deve estar com status de RECEBIDO");

            Status = Status.Cancelled;
        }

        public void Ready()
        {
            AssertionConcern.AssertArgumentEqual(Status, Status.Accepted, "Pedido deve estar com status de EM PREPARAÇÃO");

            Status = Status.Ready;
        }

        public void Close()
        {
            AssertionConcern.AssertArgumentEqual(Status, Status.Ready, "Pedido deve estar com status de PRONTO");

            Status = Status.Concluded;
        }

        public void AddItem(List<Product> products)
        {
            var itens = products.Select(product =>
                new OrderItem()
                {
                    Code = Items.Count + 1,
                    Product = product
                });


            _items.AddRange(itens);
        }

        public double TotalAmount()
        {
            return Items.Sum(item => item.Product.UnitPrice);
        }

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(_items, "Pedido não pode estar vazio");
        }
    }
}
