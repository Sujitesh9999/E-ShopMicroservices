
using Discount.Grpc;

namespace Basket.API.Basket.UpdateBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketValidator: AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage("user name cannot be null");
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        }
    }

    public class StoreBasketHandler
        (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await ApplyDiscount(request.Cart, cancellationToken);

            var result = await repository.StoreBasket(request.Cart, cancellationToken);

            return new StoreBasketResult(result.UserName);
        }

        private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var discount = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);

                item.Price -= discount.Amount;
            }
        }
    }
}
