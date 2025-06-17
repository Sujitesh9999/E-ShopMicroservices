

using Basket.API.Data;
using Mapster;
using Marten.Linq.QueryHandlers;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);


    internal class GetBasketHandler(IBasketRepository respository): IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var result = await respository.GetBasket(query.UserName, cancellationToken);

            return new GetBasketResult(result);
        }
    }
}
