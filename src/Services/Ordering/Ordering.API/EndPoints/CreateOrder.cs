
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.EndPoints
{

    public record CreateOrderRequest(OrderDto Order);

    public record CreateOrderResult(Guid Id);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest order, ISender sender ) =>
            {
                var request = order.Adapt<CreateOrderCommand>();

                var result = await sender.Send(request);

                var response = result.Adapt<CreateOrderResult>();

                return Results.Created($"orders/{response.Id}", response.Id);

            }).WithDisplayName("CreateOrder")
            .Produces<CreateOrderResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
