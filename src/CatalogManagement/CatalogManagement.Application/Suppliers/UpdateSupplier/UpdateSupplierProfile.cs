using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Suppliers.UpdateSupplier;

/// <summary>
/// Profile for mapping between Supplier entity and UpdateSupplierResponse
/// </summary>
public class UpdateSupplierProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSupplier operation
    /// </summary>
    public UpdateSupplierProfile()
    {
        CreateMap<UpdateSupplierCommand, Supplier>();
        CreateMap<Supplier, UpdateSupplierResponse>();
    }
}
