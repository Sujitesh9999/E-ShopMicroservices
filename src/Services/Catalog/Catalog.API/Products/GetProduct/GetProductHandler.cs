
using Marten.Pagination;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetProductsQueryResponse>;

    public record GetProductsQueryResponse(IEnumerable<Product> Products);


    internal class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductsQueryResponse>
    {
        public async Task<GetProductsQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.pageNumber ?? 1,request.pageSize ?? 10,cancellationToken);

            return new GetProductsQueryResponse(products);
        }
    }
}
