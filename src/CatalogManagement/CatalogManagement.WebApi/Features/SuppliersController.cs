using MediatR;
using Microsoft.AspNetCore.Mvc;
using Common.APICommon;
using CatalogManagement.Application.Suppliers.CreateSupplier;
using CatalogManagement.Application.Suppliers.GetSupplierById;
using CatalogManagement.Application.Suppliers.GetSuppliers;
using CatalogManagement.Application.Suppliers.UpdateSupplier;
using CatalogManagement.Application.Suppliers.DeleteSupplier;
using Microsoft.AspNetCore.Authorization;

namespace CatalogManagement.WebApi.Features;

/// <summary>
/// Controller for managing supplier operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/suppliers")]
public class SuppliersController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new supplier
    /// </summary>
    /// <param name="request">The supplier creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created supplier details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSupplierResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSupplierResponse>
        {
            Success = true,
            Message = "Supplier created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a supplier by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the supplier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The supplier details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSupplierByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSupplierById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetSupplierByIdQuery(id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSupplierByIdResponse>
        {
            Success = true,
            Message = "Supplier retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a list of suppliers with pagination, sorting and filtering
    /// </summary>
    /// <param name="query">The query parameters for retrieving suppliers</param>
    /// <param name="filters">Additional filters as key-value pairs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of suppliers matching the query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSuppliersResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSuppliers(
        [FromQuery] QueryStringWithFilters<GetSuppliersQuery, GetSuppliersResponse> filters,
        CancellationToken cancellationToken)
    {
        var query = filters.GetQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Update a supplier
    /// </summary>
    /// <param name="request">The supplier update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated supplier details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSupplierResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateSupplierResponse>
        {
            Success = true,
            Message = "Supplier updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Deletes a supplier by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the supplier to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the supplier was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSupplierCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Supplier deleted successfully"
        });
    }
}