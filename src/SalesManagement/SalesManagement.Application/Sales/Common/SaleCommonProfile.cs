using AutoMapper;
using SalesManagement.Application.Services.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Application.Sales.Common;

public class SaleCommonProfile : Profile
{
    public SaleCommonProfile()
    {
        CreateMap<CartItem, SaleItem>()
            .ForMember(s => s.Id, opt => opt.Ignore())
            .ForMember(s => s.Product, opt => opt.MapFrom(c => new SaleProduct { Id = c.ProductId }))
            .ForMember(s => s.Supplier, opt => opt.MapFrom(c => new SaleSupplier { Id = c.SupplierId }));

        CreateMap<Cart, Sale>()
            .ForMember(s => s.Id, opt => opt.Ignore())
            .ForMember(s => s.Status, opt => opt.Ignore())
            .ForMember(s => s.Items, opt => opt.MapFrom(c => c.Items));

        CreateMap<ProductDto, SaleProduct>()
            .ForMember(s => s.Name, opt => opt.MapFrom(p => p.Title));
        CreateMap<SupplierDto, SaleSupplier>();

        CreateMap<SaleBranch, SaleBranchResponse>();
        CreateMap<SaleItem, SaleBranchResponse>();

        CreateMap<SaleProduct, SaleProductResponse>();
        CreateMap<SaleSupplier, SaleSupplierResponse>();
        CreateMap<SaleItem, SaleItemResponse>();
    }
}
