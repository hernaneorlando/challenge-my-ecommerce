using CatalogManagement.Domain.Entities;
using Common.DomainCommon.ValueObjects;
using Bogus;
using Bogus.Extensions.Brazil;

namespace CatalogManagement.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SupplierTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Supplier entities.
    /// The generated suppliers will have valid:
    /// - Name (using company names)
    /// - Registration Number (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Supplier or Admin)
    /// </summary>
    private static readonly Faker<Supplier> SupplierFaker = new Faker<Supplier>()
        .RuleFor(u => u.Name, f => f.Company.CompanyName())
        .RuleFor(u => u.RegistrationNumber, f => f.Company.Cnpj())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => new PhoneNumber($"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}"));

    /// <summary>
    /// Generates a valid Supplier entity with randomized data.
    /// The generated Supplier will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Supplier entity with randomly generated data.</returns>
    public static Supplier GenerateValidSupplier()
    {
        return SupplierFaker.Generate();
    }
}
