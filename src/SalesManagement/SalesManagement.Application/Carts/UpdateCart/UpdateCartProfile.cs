using AutoMapper;
using SalesManagement.Application.Services.DTOs;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Cart entity and UpdateCart
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart operation
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartItemCommand, CartItem>();
        CreateMap<UpdateCartCommand, Cart>()
            .ForMember(cart => cart.Customer, opt => opt.MapFrom(c => new SaleUser { Id = c.CustomerId }))
            .ForMember(cart => cart.Items, opt => opt.MapFrom(c => c.Products.Select(p => new CartItem
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            })));

        CreateMap<ProductDto, CartItem>()
            .ForMember(c => c.ProductId, opt => opt.MapFrom(p => p.Id))
            .ForMember(c => c.UnitPrice, opt => opt.MapFrom(p => p.Price))
            .ForMember(c => c.Id, opt => opt.Ignore());

        CreateMap<CartItem, UpdateCartItemResponse>();
        CreateMap<Cart, UpdateCartResponse>()
            .ForMember(r => r.CustomerId, opt => opt.MapFrom(c => c.Customer.Id))
            .ForMember(r => r.BranchId, opt => opt.MapFrom(c => c.Branch.Id))
            .ForMember(r => r.Products, opt => opt.MapFrom(c => c.Items));
    }
}
