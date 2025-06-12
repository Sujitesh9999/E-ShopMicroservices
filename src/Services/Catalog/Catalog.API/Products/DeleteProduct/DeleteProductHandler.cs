using System.Data;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;

    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id can not be null");
        }
    }

    internal class DeleteProductCommandHandler
        (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Delete Product with id {command.Id}");

            var result = session.LoadAsync<Product>(command.Id, cancellationToken);

            if(result is null)
            {
                throw new ProductNotFoundException();
            }

            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync();

            return new DeleteProductResponse(true);
        }
    }
}
