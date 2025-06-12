namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResponse>;

    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryValidator : AbstractValidator<GetProductsByCategoryQuery>
    {
        public GetProductByCategoryValidator()
        {
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category Can not be empty")
                .Length(2,50).WithMessage("Category should be in range 2 to 50 characters");
        }
    }

    internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResponse>
    {
        public async Task<GetProductsByCategoryResponse> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(x => x.Category.Contains(request.Category))
                .ToListAsync();

            if(products is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductsByCategoryResponse(products);
        }
    }
}
