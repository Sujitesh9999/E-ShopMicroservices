namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommandResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{id}", async (Guid Id, ISender sender) =>
            {
                var response = await sender.Send(new DeleteProductCommand(Id));

                var result = response.Adapt<DeleteProductCommandResponse>();

                return Results.Ok(result);
            })
            .WithDisplayName("Delete Product")
            .Produces<DeleteProductCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
