using Framework.Domain.Models.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application.Events;

public class BackgroundDomainEventProcessor(IServiceProvider serviceProvider)
{
    public async Task ProcessEventAsync(IDomainEvent domainEvent, Type handlerType, CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        
        IEnumerable<object?> handlers = scope.ServiceProvider.GetServices(handlerType);
        
        foreach (object? handler in handlers)
        {
            if (handler is null)
            {
                continue;
            }
            
            var handlerWrapper = DomainEventDispatcher.HandlerWrapper.Create(handler, domainEvent.GetType());
            await handlerWrapper.HandleAsync(domainEvent, cancellationToken);
        }
    }
}