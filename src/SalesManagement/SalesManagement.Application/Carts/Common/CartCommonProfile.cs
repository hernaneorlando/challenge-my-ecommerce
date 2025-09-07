using AutoMapper;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Carts.Common;

public class CartCommonProfile : Profile
{
    public CartCommonProfile()
    {
        CreateMap<CartItem, CartItemResponse>();
    }
}
