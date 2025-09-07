using Common.APICommon;

namespace SalesManagement.Application.Sales.GetSales;

/// <summary>
/// Query for retrieving Sales
/// </summary>
public record GetSalesQuery : BasePagedQuery<GetSalesResponse>;