using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Product.CreateProduct
{
    public class CreateProductEndPoint : ICarterModule
    {
        public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageUrl, decimal Price);

        public record CreateProductResponse(Guid Id);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product", async(CreateProductRequest request, IMediator mediator) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await mediator.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"product/{response.Id}",response);
            }).WithDisplayName("Create Product")
            .WithDescription("Creates Product")
            .Produces(statusCode: StatusCodes.Status201Created)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
