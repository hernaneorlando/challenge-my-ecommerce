using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Suppliers.GetSupplierById;

/// <summary>
/// Profile for mapping between Supplier entity and GetSupplierByIdResponse
/// </summary>
public class GetSupplierByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSupplierById operation
    /// </summary>
    public GetSupplierByIdProfile()
    {
        CreateMap<Supplier, GetSupplierByIdResponse>();
    }
}
