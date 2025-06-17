
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var baskets = await cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(baskets))
                return JsonSerializer.Deserialize<ShoppingCart>(baskets)!;

            var result = await repository.GetBasket(UserName, cancellationToken);
            return result;

        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(UserName, cancellationToken);
            await cache.RemoveAsync(UserName);
            return true;
        }
    }
}
