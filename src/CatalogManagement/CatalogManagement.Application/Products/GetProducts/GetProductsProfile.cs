using System;
using CatalogManagement.Domain.Entities;
using Common.ORMCommon;
using AutoMapper;

namespace CatalogManagement.Application.Products.GetProducts;

public class GetProductsProfile: Profile
{
    public GetProductsProfile()
    {
        CreateMap<GetProductsQuery, PaginatedRequest>();
        CreateMap<Product, GetProductsResponse>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => (string)src.Rating! ?? string.Empty));
    }
}
