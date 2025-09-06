using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserManagement.WebApi.Features.Users.DeleteUser;
using UserManagement.Application.Users.CreateUser;
using UserManagement.Application.Users.DeleteUser;
using UserManagement.WebApi.Common;
using UserManagement.WebApi.Features.Users.CreateUser;
using UserManagement.WebApi.Features.Users.GetUser;
using Common.APICommon;
using Microsoft.AspNetCore.Authorization;
using UserManagement.Application.Users.GetUserById;
using UserManagement.Application.Users.GetUsers;
using UserManagement.Domain.Enums;
using UserManagement.Application.Users.UpdateUser;

namespace UserManagement.WebApi.Features;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Authorize(Roles = "Manager,Admin")]
[Route("api/[controller]")]
public class UsersController(IMediator _mediator, IMapper _mapper) : BaseController
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
        {
            Success = true,
            Message = "User created successfully",
            Data = _mapper.Map<CreateUserResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetUserQuery>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetUserResponse>
        {
            Success = true,
            Message = "User retrieved successfully",
            Data = _mapper.Map<GetUserResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of users with pagination, sorting and filtering
    /// </summary>
    /// <param name="query">The query parameters for retrieving users</param>
    /// <param name="filters">Additional filters as key-value pairs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of users matching the query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetUsersResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(
        [FromQuery] QueryStringWithFilters<GetUsersQuery, GetUsersResponse> filters,
        CancellationToken cancellationToken)
    {
        var query = filters.GetQuery();
        return Ok(await _mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Update a user
    /// </summary>
    /// <param name="request">The user update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateUserResponse>
        {
            Success = true,
            Message = "User updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteUserCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "User deleted successfully"
        });
    }
}
