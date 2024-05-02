using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Sanduba.Infrastructure.Persistence.SqlServer.Configurations;
using Sanduba.Core.Domain.Orders;
using Sanduba.Core.Application.Abstraction.Orders;
using System.Threading.Tasks;
using System.Threading;
using Order = Sanduba.Core.Domain.Orders.Order;
using OrderSchema = Sanduba.Infrastructure.Persistence.SqlServer.Orders.Schema.Order;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Orders
{
    public class OrderRepository(InfrastructureDbContext dbContext, IMapper mapper) : IOrderPersistence
    {
        private readonly InfrastructureDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Orders
                .Include(order => order.Items)
                .Include(order => order.Payments)
                .Where(item => item.Id == id)
                .ProjectTo<Order>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public IEnumerable<Order> GetOrdersByStatus(Status status, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Orders
                .Include(order => order.Items)
                .Include(order => order.Payments)
                .Where(item => item.Status == (int)status)
                .ToList();

            return _mapper.Map<List<Order>>(query);
        }

        public Task<IEnumerable<Order>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByClient(Guid clientId, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Orders
                .Include(order => order.Items)
                .Include(order => order.Payments)
                .Where(item => item.ClientId == clientId)
                .ToList();

            return _mapper.Map<List<Order>>(query);
        }

        public int GetNextOrderCode(CancellationToken cancellationToken = default)
        {
            return _dbContext.Orders.Count() + 1;
        }

        public Task SaveAsync(Order entity, CancellationToken cancellationToken = default)
        {
            var order = _mapper.Map<OrderSchema>(entity);
            _dbContext.Orders.Add(order);

            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(Order entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<Order> entitys, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Order> RemoveAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
