﻿using MediatR;
using Microsoft.Extensions.Logging;


namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
