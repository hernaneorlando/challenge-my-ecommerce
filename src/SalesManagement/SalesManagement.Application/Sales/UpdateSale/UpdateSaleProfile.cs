using AutoMapper;
using SalesManagement.Application.Services.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<ProductDto, SaleItem>()
            .ForMember(s => s.Id, opt => opt.Ignore())
            .ForMember(s => s.Product, opt => opt.MapFrom(p => new SaleProduct { Id = p.Id }))
            .ForMember(s => s.Supplier, opt => opt.MapFrom(p => new SaleSupplier { Id = p.SupplierId }));

        CreateMap<Sale, UpdateSaleResponse>()
            .ForMember(r => r.Products, opt => opt.MapFrom(s => s.Items));
    }
}
