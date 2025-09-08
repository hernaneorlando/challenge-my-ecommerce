using MediatR;

namespace Common.Events;

public interface IPublishDomainEvents
{
    IReadOnlyCollection<INotification> DomainEvents { get; }
    void ClearDomainEvents();
}