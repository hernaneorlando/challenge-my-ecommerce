using Common.DomainCommon.ValueObjects;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using Bogus;

namespace UserManagement.Integration.Auth.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class AuthenticateUserFeatureTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<User> createUserFaker = new Faker<User>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => new PhoneNumber($"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}"))
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin, UserRole.Manager));

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static User GenerateValidUser()
    {
        return createUserFaker.Generate();
    }

    /// <summary>
    /// Generates a valid password hash.
    /// </summary>
    /// <param name="password">Password to generate the hash.</param>
    /// <returns>A valid password hash.</returns>
    public static string GeneratePasswordHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}