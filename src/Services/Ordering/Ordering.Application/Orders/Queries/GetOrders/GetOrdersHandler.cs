
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.Request.PageIndex;
            var pageSize = query.Request.PageSize;
            var totalCount = await dbContext.Orders.LongCountAsync();

            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize,totalCount,orders.ToOrderDtoList()));
        }
    }
}
