using Sanduba.Core.Domain.Common.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Common
{
    public interface IAsyncPersistenceGateway<TId, T> where T : Entity<TId>
    {
        Task SaveAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEnumerable<T> entitys, CancellationToken cancellationToken = default);
        Task<T> RemoveAsync(TId id, CancellationToken cancellationToken = default);
        Task<T> RemoveAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
