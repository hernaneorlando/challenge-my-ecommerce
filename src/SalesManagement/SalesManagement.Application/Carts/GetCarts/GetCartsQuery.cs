using Common.APICommon;

namespace SalesManagement.Application.Carts.GetCarts;

/// <summary>
/// Query for retrieving Carts
/// </summary>
public record GetCartsQuery : BasePagedQuery<GetCartsResponse>;