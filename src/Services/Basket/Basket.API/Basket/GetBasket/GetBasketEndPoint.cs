
namespace Basket.API.Basket.GetBasket
{
   // public record GetBasketRequest(string UserName);

    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName}",async (string UserName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(UserName));

                var respone = result.Adapt<GetBasketResponse>();

                return Results.Ok(respone);
            })
            .WithDisplayName("Get Basket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
        }
    }
}
