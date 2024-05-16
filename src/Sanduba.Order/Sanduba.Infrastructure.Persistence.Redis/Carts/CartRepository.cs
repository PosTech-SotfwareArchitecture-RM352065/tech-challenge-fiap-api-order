using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System;
using Sanduba.Core.Application.Abstraction.Carts;

namespace Sanduba.Infrastructure.Persistence.Redis.Carts
{
    public class CartRepository : ICartPersistenceGateway
    {
        private readonly IDistributedCache _dbContext;
        private readonly DistributedCacheEntryOptions _options;

        public CartRepository(IDistributedCache dbContext)
        {
            _dbContext = dbContext;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
                SlidingExpiration = TimeSpan.FromMinutes(10),
            };
        }

        public void AddItem(Guid id, Guid productId)
        {
            List<Guid> list = new();

            var bytes = _dbContext.Get(id.ToString());
            if (bytes is not null && bytes.Length > 0)
            {
                var strContent = Encoding.UTF8.GetString(bytes);
                list = JsonSerializer.Deserialize<List<Guid>>(strContent);
            }
            list.Add(productId);

            var json = JsonSerializer.Serialize(list);
            _dbContext.Set(id.ToString(), Encoding.UTF8.GetBytes(json));
        }

        public List<Guid> GetSummary(Guid id)
        {
            List<Guid> list = new();

            var bytes = _dbContext.Get(id.ToString());
            if (bytes is null || bytes?.Length <= 0) return list;

            var strContent = Encoding.UTF8.GetString(bytes);
            if (string.IsNullOrEmpty(strContent)) return list;

            list = JsonSerializer.Deserialize<List<Guid>>(strContent);

            return list;
        }

        public void RemoveItem(Guid id, Guid productId)
        {
            var bytes = _dbContext.Get(id.ToString());
            if (bytes is null || bytes.Length <= 0) return;

            var strContent = Encoding.UTF8.GetString(bytes);
            var list = JsonSerializer.Deserialize<List<Guid>>(strContent);

            list.Remove(productId);

            var json = JsonSerializer.Serialize(list);
            _dbContext.Set(id.ToString(), Encoding.UTF8.GetBytes(json));
        }
    }
}
