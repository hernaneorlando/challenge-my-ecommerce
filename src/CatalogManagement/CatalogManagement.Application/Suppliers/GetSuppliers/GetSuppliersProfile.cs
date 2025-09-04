using CatalogManagement.Domain.Entities;
using Common.ORMCommon;
using AutoMapper;

namespace CatalogManagement.Application.Suppliers.GetSuppliers;

public class GetSuppliersProfile : Profile
{
    public GetSuppliersProfile()
    {
        CreateMap<Supplier, GetSuppliersResponse>();
        CreateMap<GetSuppliersQuery, PaginatedRequest>();
    }
}
