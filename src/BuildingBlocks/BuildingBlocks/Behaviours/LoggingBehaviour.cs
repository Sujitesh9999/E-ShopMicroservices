using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>
        (ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request = {Request} - Response = {Response} - RequestData = {RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The Request = {Request} took {Timetaken} Seconds",
                    typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation("[END] handled {Request} with {Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
