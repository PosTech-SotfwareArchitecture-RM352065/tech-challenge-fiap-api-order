using Sanduba.Core.Application.Abstraction.Orders.Events;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Orders
{
    public interface IOrderBroker
    {
        public Task PublishOrderCreation(OrderCreatedEvent orderCreatedEvent);
    }
}
