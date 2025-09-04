using CatalogManagement.Application.Branches.CreateBranch;
using CatalogManagement.Application.Branches.DeleteBranch;
using CatalogManagement.Application.Branches.GetBranchById;
using CatalogManagement.Application.Branches.GetBranches;
using CatalogManagement.Application.Branches.UpdateBranch;
using Common.APICommon;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogManagement.WebApi.Features;

/// <summary>
/// Controller for managing branch operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/branches")]
public class BranchesController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new branch
    /// </summary>
    /// <param name="request">The branch creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
        {
            Success = true,
            Message = "Branch created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a branch by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetBranchByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranchById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetBranchByIdQuery(id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetBranchByIdResponse>
        {
            Success = true,
            Message = "Branch retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a list of branchs with pagination, sorting and filtering
    /// </summary>
    /// <param name="query">The query parameters for retrieving branchs</param>
    /// <param name="filters">Additional filters as key-value pairs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of branchs matching the query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetBranchesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBranches(
        [FromQuery] GetBranchesQuery query,
        [FromQuery] Dictionary<string, string> filters,
        CancellationToken cancellationToken)
    {
        foreach (var filter in query.SanitizeFilters(filters))
        {
            query.AddFilter(filter.Key, filter.Value);
        }

        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Update a branch
    /// </summary>
    /// <param name="request">The branch update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBranch(Guid id, [FromBody] UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateBranchResponse>
        {
            Success = true,
            Message = "Branch updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Deletes a branch by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the branch was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteBranchCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Branch deleted successfully"
        });
    }
}