
namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var results = await session.LoadAsync<ShoppingCart>(UserName, cancellationToken);

            return results is null ? throw new BasketNotFoundException(UserName) : results;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(UserName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
