namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid Id, ISender sender) =>
            {
                var respone = await sender.Send(new GetProductsByIdQuery(Id));

                var result = respone.Adapt<GetProductByIdResponse>();

                return Results.Ok(result);
            })
            .WithDisplayName("Get Products By Id")
            .Produces<GetProductsByIdRespone>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id")
            .WithDescription("Get Products By Id");
        }
    }
}
