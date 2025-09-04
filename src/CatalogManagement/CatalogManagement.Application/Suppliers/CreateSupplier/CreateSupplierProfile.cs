using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Suppliers.CreateSupplier;

/// <summary>
/// Profile for mapping between Supplier entity and CreateSupplierResponse
/// </summary>
public class CreateSupplierProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSupplier operation
    /// </summary>
    public CreateSupplierProfile()
    {
        CreateMap<CreateSupplierCommand, Supplier>();
        CreateMap<Supplier, CreateSupplierResponse>();
    }
}
