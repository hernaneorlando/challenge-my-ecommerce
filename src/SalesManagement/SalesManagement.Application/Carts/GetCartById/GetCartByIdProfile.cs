using AutoMapper;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Profile for mapping between Cart entity and GetCartByIdResponse
/// </summary>
public class GetCartByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCartById operation
    /// </summary>
    public GetCartByIdProfile()
    {
        CreateMap<CartItem, GetCartByIdCartItemResponse>();
        CreateMap<Cart, GetCartByIdResponse>()
            .ForMember(r => r.Products, opt => opt.MapFrom(c => c.Items));
    }
}
