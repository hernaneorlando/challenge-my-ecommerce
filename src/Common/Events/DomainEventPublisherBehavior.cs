using MediatR;

namespace Common.Events;

public class DomainEventPublisherBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IPublishDomainEvents
{
    private readonly IMediator _mediator;

    public DomainEventPublisherBehavior(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        foreach (var domainEvent in response.DomainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        response.ClearDomainEvents();

        return response;
    }
}