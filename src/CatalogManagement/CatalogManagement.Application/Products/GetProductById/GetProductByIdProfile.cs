using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Products.GetProductById;

/// <summary>
/// Profile for mapping between Product entity and GetProductByIdResponse
/// </summary>
public class GetProductByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProductById operation
    /// </summary>
    public GetProductByIdProfile()
    {
        CreateMap<Product, GetProductByIdResponse>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => (string)src.Rating! ?? string.Empty));
    }
}
