
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommandRequest(Product product) : ICommand<UpdateProductCommandResponse>;

    public record UpdateProductCommandResponse(bool IsSuccess);


    public class UpdateProductValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.product.Id).NotNull().NotEmpty().WithMessage("Id can not be empty or null");
            RuleFor(x => x.product.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(x => x.product.Category).NotEmpty().WithMessage("category can not be empty");

            RuleForEach(x => x.product.Category)
                .NotEmpty()
                .Length(2, 50).WithMessage("categories can not be empty or smaller than require lenghts");

        }
    }

    internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.product.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = request.product.Name;
            product.Category = request.product.Category;
            product.Description = request.product.Description;
            product.ImageUrl = request.product.ImageUrl;
            product.Price = request.product.Price;

            session.Update<Product>(product);
            await session.SaveChangesAsync();

            return new UpdateProductCommandResponse(true);
        }
    }
}
