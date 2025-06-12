using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviours
{
    public class ValidationBehaviour<TRequest, TRespone>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TRespone>
        where TRequest : notnull, ICommand<TRespone>
    {
        public async Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationresults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationresults.Where(x => x.Errors.Any()).SelectMany(r => r.Errors).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
