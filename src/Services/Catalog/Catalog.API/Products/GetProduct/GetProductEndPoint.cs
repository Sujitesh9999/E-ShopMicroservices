namespace Catalog.API.Products.GetProduct
{
    public record GetProductRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender Sender) =>
            {
                var query = request.Adapt<GetProductQuery>();
                var respone = await Sender.Send(query);

                var result = respone.Adapt<GetProductsResponse>();

                return Results.Ok(result);
            })
            .WithDisplayName("Get Products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
