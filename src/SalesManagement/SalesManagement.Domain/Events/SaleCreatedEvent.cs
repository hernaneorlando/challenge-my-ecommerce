using MediatR;

namespace SalesManagement.Domain.Events;

public class SaleCreatedEvent(Guid cartId, Guid saleId) : INotification
{
    public Guid CartId { get; } = cartId;
    public Guid SaleId { get; } = saleId;
}
