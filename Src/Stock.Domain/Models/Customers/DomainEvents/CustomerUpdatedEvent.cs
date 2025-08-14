using Framework.Domain.Models.DomainEvents;

namespace Stock.Domain.Models.Customers.DomainEvents;

public record CustomerUpdatedEvent(Guid CustomerId) : DomainEvent(CustomerId);