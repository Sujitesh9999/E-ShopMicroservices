﻿
namespace Basket.API.Basket.UpdateBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket",async (StoreBasketRequest Cart, ISender sender) =>
            {
                var command = Cart.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            })
             .WithDisplayName("Upsert Basket")
             .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Upsert Basket")
             .WithDescription("Upsert Basket"); 
        }
    }
}
