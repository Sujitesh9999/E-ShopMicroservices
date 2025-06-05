using BuildingBlocks.CQRS;

namespace Catalog.API.Product.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
        : ICommand<CreateProductResponse>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            return new CreateProductResponse(Guid.NewGuid());
        }
    }
}
