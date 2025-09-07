using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SalesManagement.Application.Repositories;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Specifications;

namespace SalesManagement.Application.Carts.UpdateCart;

/// <summary>
/// Handler for processing UpdateCartCommand requests
/// </summary>
public class UpdateCartHandler(
    ICartRepository _cartRepository,
    ICatalogService _catalogService,
    IUserService _userService,
    IMapper _mapper) : IRequestHandler<UpdateCartCommand, UpdateCartResponse>
{
    /// <summary>
    /// Handles the UpdateCartCommand request
    /// </summary>
    /// <param name="command">The UpdateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    public async Task<UpdateCartResponse> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var userDto = await _userService.GetUserDetailsAsync(request.CustomerId);
        if(userDto.Role != "Customer")
            throw new ValidationException([new ValidationFailure(string.Empty, "The user must be a customer to own a cart")]);

        var specification = new OpenCartByCustomerSpecification(request.CustomerId);
        var existingCart = await _cartRepository.Find(specification, cancellationToken, x => x.Items)
            ?? throw new ValidationException([new ValidationFailure(string.Empty, "The Customer does not have an open cart.")]);

        var cart = _mapper.Map<Cart>(request);
        existingCart.Update(cart);

        var products = await _catalogService.GetProductDetailsAsync([.. request.Products.Select(p => p.ProductId)]);
        foreach (var product in products)
        {
            var item = _mapper.Map<CartItem>(product,
                opt => opt.AfterMap((obj, item) => item.Quantity = request.Products.FirstOrDefault(p => p.ProductId == item.ProductId)?.Quantity ?? 0));
            existingCart.AddItem(item);
        }

        existingCart.ApplyDiscount();
        var validationResult = existingCart.Validate();
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Fail to update Cart", validationResult.Errors
                .Select(e => new ValidationFailure { ErrorCode = e.Error, ErrorMessage = e.Detail }));
        }

        var updatedCart = await _cartRepository.UpdateAsync(existingCart, cancellationToken);
        return _mapper.Map<UpdateCartResponse>(updatedCart);
    }
}
