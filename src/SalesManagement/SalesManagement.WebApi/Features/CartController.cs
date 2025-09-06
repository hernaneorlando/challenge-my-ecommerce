using Common.APICommon;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Carts.CreateCart;
using SalesManagement.Application.Carts.GetCartById;
using SalesManagement.Application.Carts.GetCarts;

namespace SalesManagement.WebApi.Features;

/// <summary>
/// Controller for managing cart operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/carts")]
public class CartController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a cart by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetCartByIdQuery(id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetCartByIdResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a list of carts with pagination, sorting and filtering
    /// </summary>
    /// <param name="query">The query parameters for retrieving carts</param>
    /// <param name="filters">Additional filters as key-value pairs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of carts matching the query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetCartsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCarts(
        [FromQuery] QueryStringWithFilters<GetCartsQuery, GetCartsResponse> filters,
        CancellationToken cancellationToken)
    {
        var query = filters.GetQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }
}
