using Bogus;
using CatalogManagement.Domain.ValueObjects;
using SalesManagement.Application.Services.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Enums;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Integration.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public class CartFeatureTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated users will have valid:
    /// - Customer
    /// - Branch where the purchase is being made.
    /// - Checkout date
    /// - Status
    /// </summary>
    private static readonly Faker<Cart> createCartFaker = new Faker<Cart>()
        .RuleFor(u => u.Customer, f => createCarUserFaker!.Generate())
        .RuleFor(u => u.Branch, _ => createCartBranchFaker!.Generate())
        .RuleFor(u => u.Status, f => f.PickRandom(CartStatus.Open))
        .RuleFor(u => u.Items, f => createCartItemFaker!.Generate(5));

    private static readonly Faker<SaleUser> createCarUserFaker = new Faker<SaleUser>()
        .RuleFor(u => u.Name, f => $"{f.Person.FirstName} {f.Person.LastName}");

    private static readonly Faker<SaleBranch> createCartBranchFaker = new Faker<SaleBranch>()
        .RuleFor(u => u.Name, f => $"{f.Company.CompanyName()} {f.Address.City()}")
        .RuleFor(u => u.Code, f => f.Company.CompanySuffix());

    private static readonly Faker<CartItem> createCartItemFaker = new Faker<CartItem>()
        .RuleFor(u => u.ProductId, _ => Guid.NewGuid())
        .RuleFor(u => u.SupplierId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(u => u.UnitPrice, f => decimal.Parse(f.Commerce.Price(1, 100, 2)))
        .RuleFor(u => u.Discount, f => decimal.Parse(f.Commerce.Price(0.1M, 1M, 1)));

    private static readonly Faker<ProductDto> createProductDtoFaker = new Faker<ProductDto>()
        .RuleFor(u => u.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 100, 2)))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(u => u.SupplierId, f => Guid.NewGuid())
        .RuleFor(p => p.Rating, f => new ProductRating(f.Random.Decimal(1, 5), f.Random.Int()));

    private static readonly Faker<SupplierDto> createSupplierDtoFaker = new Faker<SupplierDto>()
        .RuleFor(u => u.Name, f => f.Company.CompanyName())
        .RuleFor(p => p.RegistrationNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(p => p.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}");

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static Cart GenerateValidCart()
    {
        return createCartFaker.Generate();
    }

    public static ProductDto GenerateValidProductDto()
    {
        return createProductDtoFaker.Generate();
    }

    public static SupplierDto GenerateValidSupplierDto()
    {
        return createSupplierDtoFaker.Generate();
    }
}
