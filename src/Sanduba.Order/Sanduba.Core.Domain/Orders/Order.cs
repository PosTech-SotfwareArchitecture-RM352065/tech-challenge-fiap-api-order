using System;
using System.Collections.Generic;
using System.Linq;
using Sanduba.Core.Domain.Payments;
using Sanduba.Core.Domain.Commons.Types;
using Sanduba.Core.Domain.Commons.Assertions;

namespace Sanduba.Core.Domain.Orders
{
    public sealed class Order : Entity<Guid>
    {
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
            AssertionConcern.AssertArgumentNotEqual(Status, Status.Payed, "Pedido deve estar com status de PAGO");

            Status = Status.Accepted;
        }

        public void Reject()
        {
            AssertionConcern.AssertArgumentNotEqual(Status, Status.Payed, "Pedido deve estar com status de PAGO");

            Status = Status.Reject;
        }

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);

            AssertionConcern.AssertArgumentNotEqual(Status, Status.Created, "Pedido deve estar com status de CRIADO");
            Status = Status.WaitingPayment;
        }

        public void Cancel()
        {
            AssertionConcern.AssertArgumentNotEqual(Status, Status, "Pedido deve estar com status de RECEBIDO");

            Status = Status.Cancelled;
        }

        public void Ready()
        {
            AssertionConcern.AssertArgumentNotEqual(Status, Status.Accepted, "Pedido deve estar com status de EM PREPARAÇÃO");

            Status = Status.Ready;
        }

        public void Close()
        {
            AssertionConcern.AssertArgumentNotEqual(Status, Status.Ready, "Pedido deve estar com status de PRONTO");

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

        public double Amount()
        {
            return Items.Sum(item => item.Product.UnitPrice);
        }

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(_items, "Pedido não pode estar vazio");
        }
    }
}
