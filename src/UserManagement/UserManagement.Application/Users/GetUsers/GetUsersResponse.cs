namespace UserManagement.Application.Users.GetUsers;

/// <summary>
/// API response model for GetUsers operation
/// </summary>
public record GetUsersResponse
{
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The username
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system.
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The user's current status.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the user was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time of the last update to the user's information
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
