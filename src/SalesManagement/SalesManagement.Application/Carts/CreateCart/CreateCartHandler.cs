using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SalesManagement.Application.Repositories;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Specifications;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Application.Carts.CreateCart;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateCartHandler(
    ICartRepository _cartRepository,
    ICatalogService _catalogService,
    IUserService _userService,
    IMapper _mapper) : IRequestHandler<CreateCartCommand, CreateCartResponse>
{
    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<CreateCartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var userDto = await _userService.GetUserDetailsAsync(request.CustomerId);
        if(userDto.Role != "Customer")
            throw new ValidationException([new ValidationFailure(string.Empty, "The user must be a customer to own a cart")]);

        var specification = new OpenCartByCustomerSpecification(request.CustomerId);
        var existentCart = await _cartRepository.Find(specification, cancellationToken);
        if (existentCart is not null)
            throw new ValidationException([new ValidationFailure(string.Empty, $"The Customer with ID {request.CustomerId} already has an open cart")]);

        var branchDto = await _catalogService.GetBranchDetailsAsync(request.BranchId);
        
        var cart = _mapper.Map<Cart>(request);
        var branch = _mapper.Map<SaleBranch>(branchDto);
        var user = _mapper.Map<SaleUser>(userDto);
        cart.Create(user, branch);

        var products = await _catalogService.GetProductDetailsAsync([.. request.Products.Select(p => p.ProductId)]);
        foreach (var product in products)
        {
            var item = _mapper.Map<CartItem>(product,
                opt => opt.AfterMap((obj, item) => item.Quantity = request.Products.FirstOrDefault(p => p.ProductId == item.ProductId)?.Quantity ?? 0));
            cart.AddItem(item);
        }

        cart.ApplyDiscount();
        var validationResult = cart.Validate();
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Fail to create Cart", validationResult.Errors
                .Select(e => new ValidationFailure { ErrorCode = e.Error, ErrorMessage = e.Detail }));
        }

        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);
        return _mapper.Map<CreateCartResponse>(createdCart);
    }
}