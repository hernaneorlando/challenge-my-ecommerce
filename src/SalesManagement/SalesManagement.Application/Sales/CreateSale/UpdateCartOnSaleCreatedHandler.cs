using Common.Events;
using FluentValidation;
using FluentValidation.Results;
using SalesManagement.Application.Repositories;
using SalesManagement.Domain.Enums;
using SalesManagement.Domain.Events;

namespace SalesManagement.Application.Sales.CreateSale;

public class UpdateCartOnSaleCreatedHandler(ICartRepository _cartRepository) : IHandleDomainEvent<SaleCreatedEvent>
{
    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(notification.CartId, cancellationToken)
            ?? throw new ValidationException([new ValidationFailure(string.Empty, $"The Cart with ID {notification.CartId} does not exist.")]);

        cart.Status = CartStatus.CheckedOut;
        cart.CheckoutDate = DateOnly.FromDateTime(DateTime.UtcNow);
        cart.UpdatedAt = DateTime.UtcNow;
        
        await _cartRepository.UpdateAsync(cart, cancellationToken);
    }
}