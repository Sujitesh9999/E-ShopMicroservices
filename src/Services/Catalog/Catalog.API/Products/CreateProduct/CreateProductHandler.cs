

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
        : ICommand<CreateProductResponse>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty")
                .MaximumLength(200).WithMessage("Max length is 200 characters");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image can not be empty");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
        }
    }

    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.Adapt<Product>();

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResponse(product.Id);
        }
    }
}
