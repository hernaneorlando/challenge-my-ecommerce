using CatalogManagement.Application.Suppliers.CreateSupplier;
using Bogus;
using Bogus.Extensions.Brazil;

namespace CatalogManagement.Unit.Application.Suppliers.TestData;

public static class CreateSupplierHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Supplier entities.
    /// The generated suppliers will have valid:
    /// - Name (using company names)
    /// - RegistrationNumber (using company CNPJ)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// </summary>
    private static readonly Faker<CreateSupplierCommand> createSupplierHandlerFaker = new Faker<CreateSupplierCommand>()
        .RuleFor(u => u.Name, f => f.Company.CompanyName())
        .RuleFor(u => u.RegistrationNumber, f => f.Company.Cnpj())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}");

    /// <summary>
    /// Generates a valid Supplier entity with randomized data.
    /// The generated supplier will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Supplier entity with randomly generated data.</returns>
    public static CreateSupplierCommand GenerateValidCommand()
    {
        return createSupplierHandlerFaker.Generate();
    }
}
