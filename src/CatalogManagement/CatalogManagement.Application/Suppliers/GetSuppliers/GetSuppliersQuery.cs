using Common.APICommon;

namespace CatalogManagement.Application.Suppliers.GetSuppliers;

/// <summary>
/// Query for retrieving Suppliers
/// </summary>
public record GetSuppliersQuery : BasePagedQuery<GetSuppliersResponse>;