using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Entities.ValueObjects;
using AutoMapper;

namespace CatalogManagement.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => new ProductRating(src.Rating ?? 0, src.RatingCount ?? 0)));

        CreateMap<Product, UpdateProductResponse>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => (string)src.Rating! ?? string.Empty));
    }
}
