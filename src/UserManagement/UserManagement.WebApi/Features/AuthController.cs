using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Auth.AuthenticateUser;
using UserManagement.WebApi.Common;
using Common.APICommon;

namespace UserManagement.WebApi.Features;

/// <summary>
/// Controller for authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator) : BaseController
{
    /// <summary>
    /// Authenticates a user with their credentials
    /// </summary>
    /// <param name="request">The authentication request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication token if successful</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResponseWithData<AuthenticateUserResult>
        {
            Success = true,
            Message = "User authenticated successfully",
            Data = response
        });
    }
}
