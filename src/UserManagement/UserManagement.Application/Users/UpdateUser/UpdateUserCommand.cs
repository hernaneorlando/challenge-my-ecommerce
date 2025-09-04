using System.Text.Json.Serialization;
using UserManagement.Domain.Enums;
using MediatR;

namespace UserManagement.Application.Users.UpdateUser;

/// <summary>
/// Command for updating a user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a user, 
/// including username, password, phone number, email, status, and role.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateUserResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateUserValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record UpdateUserCommand : IRequest<UpdateUserResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to be updated.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number for the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public UserRole Role { get; set; }
}
