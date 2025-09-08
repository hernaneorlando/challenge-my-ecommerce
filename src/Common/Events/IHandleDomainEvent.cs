using MediatR;

namespace Common.Events;

public interface IHandleDomainEvent<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{ }