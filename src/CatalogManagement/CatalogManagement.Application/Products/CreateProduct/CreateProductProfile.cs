using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Entities.ValueObjects;
using AutoMapper;

namespace CatalogManagement.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Product entity and CreateProductResponse
/// </summary>
public class CreateProductProfile : Profile
{
/// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => new ProductRating(src.Rating ?? 0, src.RatingCount ?? 0)));

        CreateMap<Product, CreateProductResponse>()
            .ForMember(p => p.Rating, opt => opt.MapFrom(src => (string)src.Rating! ?? string.Empty));
    }
}
