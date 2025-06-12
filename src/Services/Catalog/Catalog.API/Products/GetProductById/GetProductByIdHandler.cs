namespace Catalog.API.Products.GetProductById
{
    public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductsByIdRespone>;

    public record GetProductsByIdRespone(Product Product);

    public class GetProductByIdValidator: AbstractValidator<GetProductsByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id can not be null");
        }
    }

    internal class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductsByIdQuery, GetProductsByIdRespone>
    {
        public async Task<GetProductsByIdRespone> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductsByIdRespone(product);
        }
    }
}
