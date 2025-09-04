using Common.APICommon;

namespace CatalogManagement.Application.Products.GetProducts;

/// <summary>
/// Query for retrieving Products
/// </summary>
public record GetProductsQuery : BasePagedQuery<GetProductsResponse>;
