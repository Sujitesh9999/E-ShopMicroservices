﻿
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.EndPoints
{
    public record UpdateOrderRequest(OrderDto Order);

    public record UpdateOrderResponse(bool IsSuccess);


    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest order, ISender sender) =>
            {
                var request = order.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(request);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);

            }).WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order"); 
        }
    }
}
