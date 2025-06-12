
using Catalog.API.Products.DeleteProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (Product product, ISender sender) =>
            {
                var response = await sender.Send(new UpdateProductCommandRequest(product));

                return Results.Ok(response);
            })
            .WithDisplayName("Update Product")
            .Produces<UpdateProductCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product"); 
        }
    }
}
