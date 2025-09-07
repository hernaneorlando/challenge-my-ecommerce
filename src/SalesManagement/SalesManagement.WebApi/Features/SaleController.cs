using Common.APICommon;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Sales.CreateSale;

namespace SalesManagement.WebApi.Features;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/sales")]
public class SaleController(IMediator _mediator) : Controller
{
    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = response
        });
    }
}
