using AutoMapper;
using Common.ORMCommon;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Carts.GetCarts;

public class GetCartsProfile : Profile
{
    public GetCartsProfile()
    {
        CreateMap<Cart, GetCartsResponse>();
        CreateMap<GetCartsQuery, PaginatedRequest>();
    }
}
