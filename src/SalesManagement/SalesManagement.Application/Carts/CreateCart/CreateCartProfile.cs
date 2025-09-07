using AutoMapper;
using SalesManagement.Application.Services.DTOs;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Carts.CreateCart;

/// <summary>
/// Profile for mapping between Cart entity and CreateCart
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart operation
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartItemCommand, CartItem>();
        CreateMap<CreateCartCommand, Cart>()
            .ForMember(cart => cart.Customer, opt => opt.MapFrom(c => new SaleUser { Id = c.CustomerId }))
            .ForMember(cart => cart.Branch, opt => opt.MapFrom(c => new SaleBranch { Id = c.BranchId }))
            .ForMember(cart => cart.Items, opt => opt.MapFrom(c => c.Products.Select(p => new CartItem
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            })));

        CreateMap<BranchDto, SaleBranch>();
        CreateMap<UserDto, SaleUser>();
        CreateMap<ProductDto, CartItem>()
            .ForMember(c => c.ProductId, opt => opt.MapFrom(p => p.Id))
            .ForMember(c => c.UnitPrice, opt => opt.MapFrom(p => p.Price))
            .ForMember(c => c.Id, opt => opt.Ignore());

        CreateMap<Cart, CreateCartResponse>()
            .ForMember(r => r.CustomerId, opt => opt.MapFrom(c => c.Customer.Id))
            .ForMember(r => r.BranchId, opt => opt.MapFrom(c => c.Branch.Id))
            .ForMember(r => r.Products, opt => opt.MapFrom(c => c.Items));
    }
}
